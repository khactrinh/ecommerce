using Ecommerce.Domain.Catalog.Categories;
using Ecommerce.Domain.Catalog.Products.Events;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Events;

namespace Ecommerce.Domain.Catalog.Products;

public class Product : BaseEntity
{
    public Guid Id { get; private set; } 
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string ImageUrl { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;
    
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt  { get; private set; }

    private Product() { }

    private Product(string name, decimal price, int stock, string imageUrl, Guid categoryId)
    {
        Id = Guid.NewGuid(); // 🔥 FIX
        Name = name;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
        CreatedAt = DateTime.UtcNow;
    }

    // 🔥 FACTORY
    public static Product Create(
        string name,
        decimal price,
        int stock,
        string imageUrl,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (price <= 0)
            throw new ArgumentException("Invalid price");

        if (stock < 0)
            throw new ArgumentException("Invalid stock");

        var product = new Product(name, price, stock, imageUrl, categoryId);

        product.AddDomainEvent(
            new ProductCreatedEvent(product.Id, product.Name)
        );

        return product;
    }
    

    // 🔥 BEHAVIOR

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Invalid price");

        Price = price;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductPriceUpdatedEvent(Id, price));
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        Stock += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Invalid quantity");

        if (Stock < quantity)
            throw new InvalidOperationException("Not enough stock");

        Stock -= quantity;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductStockDecreasedEvent(Id, quantity));
    }

    public void ChangeCategory(Guid categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        Name = name;
    }
    
    
    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
    
    public void Update(
        string name,
        decimal price,
        int stock,
        string imageUrl,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (price <= 0)
            throw new ArgumentException("Price must be greater than 0");

        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative");

        Name = name;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
        CategoryId = categoryId;

        UpdatedAt = DateTime.UtcNow; // nếu bạn có field này
    }
}