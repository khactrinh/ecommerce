using System.Text.Json;
using Ecommerce.Application.Common.Events;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Orders.Sagas;
using Ecommerce.Domain.Cart;
using Ecommerce.Domain.Catalog.Categories;
using Ecommerce.Domain.Catalog.Products;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Identity;
using Ecommerce.Domain.Orders;
using MediatR;

namespace Ecommerce.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IMediator? _mediator;
    
    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        IMediator? mediator = null) // 👈 optional
        : base(options)
    {
        _mediator = mediator;
    }

    // 🧱 Write side (EF Core)
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    public DbSet<Category> Categories => Set<Category>();
    //public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    
    public DbSet<OrderSagaState> OrderSagas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.UseSnakeCaseNamingConvention();
        
        base.OnModelCreating(modelBuilder);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Ignore(nameof(BaseEntity.DomainEvents));
            }
        }

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        
        //TODO: test Outbox
        // var outboxMessages = domainEvents.Select(e => new OutboxMessage
        // {
        //     Id = Guid.NewGuid(),
        //     Type = e.GetType().Name,
        //     Content = JsonSerializer.Serialize(e),
        //     OccurredOn = e.OccurredOn
        // }).ToList();
        // OutboxMessages.AddRange(outboxMessages);
        
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
        
        // 🔥 Convert event → OutboxMessage
        var outboxMessages = domainEvents.Select(e => new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOn = e.OccurredOn,
            Type = e.GetType().AssemblyQualifiedName!,
            Content = JsonSerializer.Serialize(e)
        }).ToList();

        await Set<OutboxMessage>().AddRangeAsync(outboxMessages, cancellationToken);
        
        var result = await base.SaveChangesAsync(cancellationToken);

        if (_mediator != null) // 👈 fix null
        {
            await DispatchDomainEvents(cancellationToken);
        }
        
        return result;
    }
    
    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        var entities = ChangeTracker
            .Entries<BaseEntity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var entity in entities)
            entity.Entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            var notification = (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>)
                    .MakeGenericType(domainEvent.GetType()),
                domainEvent)!;

            await _mediator.Publish(notification, cancellationToken);
        }
    }
}