using MediatR;

namespace Ecommerce.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string? Description) : IRequest<Guid>;