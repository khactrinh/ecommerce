namespace Ecommerce.Application.Catalog.Products.Commands;

using MediatR;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Interfaces;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repo;

    public CreateProductHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price, request.Stock);

        await _repo.AddAsync(product);

        return product.Id;
    }
}