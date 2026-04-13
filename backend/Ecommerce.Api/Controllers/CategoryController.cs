using Ecommerce.Application.Catalog.Categories.Commands.DeleteCategory;
using Ecommerce.Application.Catalog.Categories.Commands.UpdateCategory;
using Ecommerce.Application.Catalog.Categories.Queries.GetCategories;
using Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;
using Ecommerce.Application.Features.Categories.Commands.CreateCategory;
using Ecommerce.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CategoryFilter filter)
    {
        var result = await _mediator.Send(new GetCategoriesQuery(filter));
        return Ok(result);
    }
    
    [HttpGet("tree")]
    public async Task<IActionResult> GetTree()
    {
        var result = await _mediator.Send(new GetCategoryTreeQuery());
        return Ok(result);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _mediator.Send(new GetCategoryByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }
}