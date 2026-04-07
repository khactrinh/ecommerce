using MediatR;

namespace Ecommerce.Application.Catalog.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;