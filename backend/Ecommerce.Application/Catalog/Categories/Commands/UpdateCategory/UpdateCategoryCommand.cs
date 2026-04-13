using MediatR;

namespace Ecommerce.Application.Catalog.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name, string? Description) : IRequest<Unit>;