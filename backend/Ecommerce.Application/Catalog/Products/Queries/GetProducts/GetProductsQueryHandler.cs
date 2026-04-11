using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using MediatR;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Shared.Pagination;

namespace Ecommerce.Application.Catalog.GetProducts;

ublic class GetProductsHandler 
    : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
{
    private readonly IProductReadService _readService;

    public GetProductsHandler(IProductReadService readService)
    {
        _readService = readService;
    }

    public async Task<PagedResult<ProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var filter = request.Filter;

        var (items, total) = await _readService.GetProductsAsync(filter);

        var responseItems = items.Select(x => new ProductResponse(
            x.Id,
            x.Name,
            x.Price
        )).ToList();

        return PagedResult<ProductResponse>.Create(
            responseItems,
            total,
            filter.Page,
            filter.PageSize
        );
    }
}