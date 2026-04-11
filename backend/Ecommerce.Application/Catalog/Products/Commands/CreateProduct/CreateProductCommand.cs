using MediatR;

namespace Ecommerce.Application.Catalog.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    int Stock,
    string ImageUrl,
    Guid CategoryId
) : IRequest<Guid>;

//TODO: public record CreateProductCommand(...) : IRequest<Guid>, ICommand;