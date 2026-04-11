using MediatR;

namespace Ecommerce.Application.Catalog.Products.Commands.CreateProduct;

public record Command(string Name, decimal Price, int Stock, string ImageUrl, Guid CategoryId) : IRequest<Guid>;