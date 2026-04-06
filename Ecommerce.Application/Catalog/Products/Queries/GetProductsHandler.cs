using Ecommerce.Application.Catalog.Products.DTOs;
using Ecommerce.Application.Catalog.Products.Interfaces;
using MediatR;

namespace Ecommerce.Application.Catalog.Products.Queries;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductReadService _readService;

    public GetProductsHandler(IProductReadService readService)
    {
        _readService = readService;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _readService.GetProductsAsync(request.Page, request.PageSize);
    }
}