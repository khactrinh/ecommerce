using Ecommerce.Application.Common.Models;
using MediatR;
using Ecommerce.Application.Common.Extensions;

namespace Ecommerce.Application.Catalog.GetProducts;

public class GetProductsHandler 
    : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductReadService _service;

    public GetProductsHandler(IProductReadService service)
    {
        _service = service;
    }

    public async Task<PagedResult<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var (items, total) = await _service.GetProductsAsync(
            request.Page,
            request.PageSize,
            request.Search
        );

        //var mapper = new ProductMapper();
        //return items.Map(p => mapper.ToDto(p));
        
        return new PagedResult<ProductDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total
        };
    }
}