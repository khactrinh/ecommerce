namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public record ProductListItemResponse(
    Guid Id,
    string Name,
    decimal Price,
    string ImageUrl,
    int Stock
);