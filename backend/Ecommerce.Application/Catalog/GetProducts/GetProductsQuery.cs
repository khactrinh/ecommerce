using Ecommerce.Application.Common.Models;
using MediatR;

namespace Ecommerce.Application.Catalog.GetProducts;

public record GetProductsQuery(
    int Page = 1,
    int PageSize = 12,
    string? Search = null
) : IRequest<PagedResult<ProductDto>>;