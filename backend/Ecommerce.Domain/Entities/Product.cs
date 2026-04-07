namespace Ecommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    
    public string ImageUrl { get; private set; }

    private Product() { }

    public Product(string name, decimal price, int stock, string imageUrl)
    {
        Name = name;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
    }
    
    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}