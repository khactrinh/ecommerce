namespace Ecommerce.Application.Catalog.Products.DTOs;

public record ProductResponse(
    Guid Id,
    string Name,
    decimal Price
);