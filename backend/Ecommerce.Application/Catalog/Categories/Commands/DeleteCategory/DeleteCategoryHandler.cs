using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Catalog.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public DeleteCategoryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category == null)
            throw new CategoryNotFoundException(request.Id);

        var hasProducts = await _context.Products
            .AnyAsync(x => x.CategoryId == request.Id, cancellationToken);

        if (hasProducts)
            throw new InvalidOperationException("Cannot delete category with products");
        
        var hasChildren = await _context.Categories
            .AnyAsync(x => x.ParentId == request.Id, cancellationToken);

        if (hasChildren)
            throw new InvalidOperationException("Cannot delete category with children");

        category.Delete();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

