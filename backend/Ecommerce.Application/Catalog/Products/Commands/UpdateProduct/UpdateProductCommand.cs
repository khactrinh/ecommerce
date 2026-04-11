using MediatR;

namespace Ecommerce.Application.Catalog.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    int Stock,
    string ImageUrl,
    Guid CategoryId
) : IRequest;