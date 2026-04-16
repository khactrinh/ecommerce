using MediatR;

namespace Ecommerce.Application.Carts.Commands.RemoveItem;

public record RemoveItemCommand(Guid UserId, Guid ProductId) : IRequest;
