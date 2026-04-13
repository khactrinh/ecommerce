using Ecommerce.Shared.Pagination;
using MediatR;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategories;

public record GetCategoriesQuery(CategoryFilter Filter)
    : IRequest<PagedResult<CategoryListItemResponse>>;