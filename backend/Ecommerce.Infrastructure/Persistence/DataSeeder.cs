using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new List<Product>
        {
            new Product("iPhone 15", 25000000, 10, "https://picsum.photos/300/300?random=1"),
            new Product("Samsung Galaxy S23", 20000000, 15, "https://picsum.photos/300/300?random=1"),
            new Product("MacBook Pro M3", 45000000, 5, "https://picsum.photos/300/300?random=1"),
            new Product("Dell XPS 13", 35000000, 7, "https://picsum.photos/300/300?random=1"),
            new Product("iPad Pro", 22000000, 12, "https://picsum.photos/300/300?random=1"),
            new Product("AirPods Pro", 6000000, 20, "https://picsum.photos/300/300?random=1"),
            new Product("Logitech MX Master 3", 2500000, 25, "https://picsum.photos/300/300?random=1"),
            new Product("Mechanical Keyboard", 3000000, 30, "https://picsum.photos/300/300?random=1"),
            new Product("Gaming Monitor", 8000000, 8, "https://picsum.photos/300/300?random=1"),
            new Product("Sony WH-1000XM5", 9000000, 9, "https://picsum.photos/300/300?random=1")
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}