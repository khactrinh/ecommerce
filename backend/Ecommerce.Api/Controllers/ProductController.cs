using Ecommerce.Application.Catalog.GetProductById;
using Ecommerce.Application.Catalog.GetProducts;
using Ecommerce.Application.Catalog.Products.Commands;
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
        return Ok(id);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        [FromQuery] string? search = null)
    {
        var result = await _mediator.Send(
            new GetProductsQuery(page, pageSize, search));

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}