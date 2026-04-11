namespace Ecommerce.Application.Catalog.Products.Queries.GetProductById;

public record ProductDetailResponse(
    // Guid Id,
    // string Name,
    // decimal Price,
    // int Stock,
    // string ImageUrl,
    // Guid CategoryId
    
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    string ImageUrl,
    Guid CategoryId
);