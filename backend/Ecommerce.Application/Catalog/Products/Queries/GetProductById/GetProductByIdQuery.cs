using MediatR;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailResponse>;