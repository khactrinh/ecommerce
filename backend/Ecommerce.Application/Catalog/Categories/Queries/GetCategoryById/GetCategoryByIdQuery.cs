using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;