using Ecommerce.Shared.Pagination;
using MediatR;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProducts;

public class GetProductsQueryHandler 
    : IRequestHandler<GetProductsQuery, PagedResult<ProductListItemResponse>>
{
    private readonly IGetProductsQueryService _queryService;


    public GetProductsQueryHandler(IGetProductsQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<PagedResult<ProductListItemResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var (items, total) = await _queryService.Execute(request.Filter);

        var result = items.Select(x => new ProductListItemResponse(
            x.Id,
            x.Name,
            x.Price,
            x.ImageUrl
        )).ToList();

        return PagedResult<ProductListItemResponse>.Create(
            result,
            total,
            request.Filter.Page,
            request.Filter.PageSize
        );
    }
}