using MediatR;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Catalog.Categories;

namespace Ecommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateCategoryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.Description);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}