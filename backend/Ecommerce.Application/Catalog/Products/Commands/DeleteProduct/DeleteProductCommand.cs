using MediatR;

namespace Ecommerce.Application.Catalog.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;