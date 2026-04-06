using MediatR;

namespace Ecommerce.Application.Orders.CreateOrder;

public record CreateOrderCommand(Guid UserId) : IRequest<Guid>;