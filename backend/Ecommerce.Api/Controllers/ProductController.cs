using Ecommerce.Application.Catalog.Products.Commands.CreateProduct;
using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;
using Ecommerce.Shared.Pagination;
using Ecommerce.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

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

        return Ok(ApiResponse<Guid>.Ok(id, "Product created successfully"));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] ProductFilter filter)
    {
        var result = await _mediator.Send(new GetProductsQuery(filter));

        return Ok(ApiResponse<PagedResult<ProductListItemResponse>>
            .Ok(result));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        if (result is null)
        {
            return NotFound(ApiResponse<string>
                .Fail("Product not found"));
        }

        return Ok(ApiResponse<ProductDetailResponse>
            .Ok(result));
    }
}