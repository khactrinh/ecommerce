using Ecommerce.Application.Catalog.Products.DTOs;

namespace Ecommerce.Application.Catalog.Products.Queries;

using MediatR;

public record GetProductsQuery(int Page, int PageSize) : IRequest<IEnumerable<ProductDto>>;