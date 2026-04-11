using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Catalog.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler 
    : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(request.Id);

        product.UpdatePrice(request.Price);
        product.IncreaseStock(request.Stock);
        
        
        // product.Update(
        //     request.Name,
        //     request.Price,
        //     request.Stock,
        //     request.ImageUrl,
        //     request.CategoryId
        // );
        //
        
    }
}