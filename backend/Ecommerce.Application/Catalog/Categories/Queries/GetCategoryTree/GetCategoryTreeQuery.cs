using MediatR;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;

public record GetCategoryTreeQuery 
    : IRequest<List<CategoryTreeResponse>>;