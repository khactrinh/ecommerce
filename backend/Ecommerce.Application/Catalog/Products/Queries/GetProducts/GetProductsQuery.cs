using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Shared.Pagination;
using MediatR;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public record GetProductsQuery(ProductFilter Filter) 
    : IRequest<PagedResult<ProductListItemResponse>>;