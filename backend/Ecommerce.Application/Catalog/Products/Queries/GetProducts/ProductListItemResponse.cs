namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public record GetProductsResponse(
    Guid Id,
    string Name,
    decimal Price,
    string ImageUrl
);