using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Catalog.Products.Commands;
using Ecommerce.Application.Catalog.Products.Commands.CreateProduct;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Ecommerce.Shared.Pagination;
using Ecommerce.Shared.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<Guid>.SuccessResponse(id));
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] ProductFilter filter)
    {
        var result = await _mediator.Send(new GetProductsQuery(filter));

        return Ok(ApiResponse<PagedResult<ProductListItemResponse>>
            .SuccessResponse(result));
    }

    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        return Ok(ApiResponse<ProductDetailResponse>
            .SuccessResponse(result));
    }
}