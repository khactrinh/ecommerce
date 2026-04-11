using Ecommerce.Application.Catalog.Products.Commands.CreateProduct;
using Ecommerce.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Catalog.Products.Commands;

using MediatR;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Interfaces;

public class CreateProductCommandHandler 
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            throw new CategoryNotFoundException(request.CategoryId);

        var product = Product.Create(
            request.Name,
            request.Price,
            request.Stock,
            request.ImageUrl,
            request.CategoryId
        );

        await _context.Products.AddAsync(product, cancellationToken);

        return product.Id;
    }
}