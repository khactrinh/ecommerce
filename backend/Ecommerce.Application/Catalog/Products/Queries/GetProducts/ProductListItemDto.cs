namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public class ProductListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; } 
    public string ImageUrl { get; set; }
    
}

//TODO : public record ProductDetailDto(Guid Id, string Name, decimal Price, Guid CategoryId);