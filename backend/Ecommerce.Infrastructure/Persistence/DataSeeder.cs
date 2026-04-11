using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Identity;

namespace Ecommerce.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // =========================
        // 🔥 Seed Roles
        // =========================
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role("Admin"),
                new Role("Customer")
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        // =========================
        // 🔥 Seed Admin User
        // =========================
        if (!context.Users.Any(x => x.Email == "admin@gmail.com"))
        {
            var adminRole = context.Roles.First(x => x.Name == "Admin");

            var user = User.Create(
                "admin@gmail.com",
                BCrypt.Net.BCrypt.HashPassword("123456")
            );

            user.AddRole(adminRole.Id);

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        // =========================
        // 🔥 Seed Products
        // =========================
        if (!context.Products.Any())
        {
            var products = new List<Product>();

            for (int i = 1; i <= 10; i++)
            {
                products.Add(new Product(
                    $"Product {i}",
                    1000000 * i,
                    10 + i,
                    $"https://picsum.photos/300/300?random={i}",
                    Guid.NewGuid() //TODO
                ));
            }

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}