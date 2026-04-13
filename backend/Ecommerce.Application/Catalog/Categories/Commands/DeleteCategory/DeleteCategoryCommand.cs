using MediatR;

namespace Ecommerce.Application.Catalog.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;