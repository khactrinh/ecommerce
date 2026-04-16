using MediatR;

namespace Ecommerce.Application.Orders.Commands.Checkout;

public record CheckoutCommand(
    Guid UserId,
    string IpAddress
) : IRequest<CheckoutResponse>;