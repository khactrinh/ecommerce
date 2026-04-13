using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Cart;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Identity;
using MediatR;

namespace Ecommerce.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;
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
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        //TODO: test Outbox
        // var outboxMessages = domainEvents.Select(e => new OutboxMessage
        // {
        //     Id = Guid.NewGuid(),
        //     Type = e.GetType().Name,
        //     Content = JsonSerializer.Serialize(e),
        //     OccurredOn = e.OccurredOn
        // }).ToList();
        // OutboxMessages.AddRange(outboxMessages);
        
        
        var result = await base.SaveChangesAsync(cancellationToken);

        if (_mediator != null) // 👈 fix null
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
        
        foreach (var entity in ChangeTracker.Entries<BaseEntity>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}