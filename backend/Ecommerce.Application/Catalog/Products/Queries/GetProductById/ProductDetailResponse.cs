namespace Ecommerce.Application.Catalog.Products.Queries.GetProductById;

public record GetProductByIdResponse(
    // Guid Id,
    // string Name,
    // decimal Price,
    // int Stock,
    // string ImageUrl,
    // Guid CategoryId
    
    Guid Id,
    string Name,
    decimal Price,
    string ImageUrl
);