namespace Ecommerce.Application.Catalog.Products.Commands;

using MediatR;

public record CreateProductCommand(string Name, decimal Price, int Stock, string ImageUrl) : IRequest<Guid>;