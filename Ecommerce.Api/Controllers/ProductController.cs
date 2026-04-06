using Ecommerce.Application.Catalog.Products.Commands;
using Ecommerce.Application.Catalog.Products.Queries;

namespace Ecommerce.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;

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
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var result = await _mediator.Send(new GetProductsQuery(page, pageSize));
        return Ok(result);
    }
}