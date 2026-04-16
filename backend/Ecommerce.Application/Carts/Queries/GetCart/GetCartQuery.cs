using MediatR;

namespace Ecommerce.Application.Carts.Queries.GetCart;

public record GetCartQuery(Guid UserId) : IRequest<List<CartResponse>>;

public record CartResponse(
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    string ImageUrl);
