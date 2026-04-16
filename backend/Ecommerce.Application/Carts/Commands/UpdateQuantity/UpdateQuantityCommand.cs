using MediatR;

namespace Ecommerce.Application.Carts.Commands.UpdateQuantity;

public record UpdateQuantityCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest;
