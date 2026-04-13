using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Exceptions;
using MediatR;

namespace Ecommerce.Application.Catalog.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

        if (category == null)
            throw new CategoryNotFoundException(request.Id);

        category.Update(request.Name, request.Description);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}