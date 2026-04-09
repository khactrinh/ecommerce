using Ecommerce.Domain.Cart;
using Ecommerce.Domain.Identity;

namespace Ecommerce.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;
using EFCore.NamingConventions;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.UseSnakeCaseNamingConvention();
        
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}