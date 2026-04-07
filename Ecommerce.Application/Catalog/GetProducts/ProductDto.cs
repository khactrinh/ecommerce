namespace Ecommerce.Application.Catalog.GetProducts;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; } 
}