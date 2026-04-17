using Ecommerce.Application.Catalog.Categories.Commands.DeleteCategory;
using Ecommerce.Application.Catalog.Categories.Commands.UpdateCategory;
using Ecommerce.Application.Catalog.Categories.Dtos;
using Ecommerce.Application.Catalog.Categories.Queries.GetCategories;
using Ecommerce.Application.Catalog.Categories.Queries.GetCategoryById;
using Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;
using Ecommerce.Application.Features.Categories.Commands.CreateCategory;
using Ecommerce.Shared.Pagination;
using Ecommerce.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] CategoryFilter filter)
    {
        var result = await _mediator.Send(new GetCategoriesQuery(filter));

        return Ok(ApiResponse<PagedResult<CategoryResponse>>
            .Ok(result));
    }

    [HttpGet("tree")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTree()
    {
        var result = await _mediator.Send(new GetCategoryTreeQuery());

        return Ok(ApiResponse<List<CategoryTreeResponse>>
            .Ok(result));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));

        if (result is null)
        {
            return NotFound(ApiResponse<string>
                .Fail("Category not found"));
        }

        return Ok(ApiResponse<CategoryDetailResponse>
            .Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(ApiResponse<Guid>
            .Ok(id, "Category created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse<string>
                .Fail("Invalid category id"));
        }

        await _mediator.Send(command);

        return Ok(ApiResponse<string>
            .Ok("Category updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));

        return Ok(ApiResponse<string>
            .Ok("Category deleted successfully"));
    }
}