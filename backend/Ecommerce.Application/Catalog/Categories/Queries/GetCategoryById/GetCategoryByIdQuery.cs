using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using MediatR;

using Ecommerce.Application.Catalog.Categories.Dtos;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id)
    : IRequest<CategoryDetailResponse>;