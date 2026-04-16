using MediatR;

namespace Ecommerce.Application.Carts.Commands.AddToCart;

public record AddToCartCommand(
    Guid UserId,
    Guid ProductId,
    int Quantity
) : IRequest;