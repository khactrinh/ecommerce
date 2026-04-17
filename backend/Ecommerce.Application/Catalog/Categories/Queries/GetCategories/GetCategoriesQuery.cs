using Ecommerce.Shared.Pagination;
using MediatR;
using Ecommerce.Application.Catalog.Categories.Dtos;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategories;

public record GetCategoriesQuery(CategoryFilter Filter)
    : IRequest<PagedResult<CategoryResponse>>;