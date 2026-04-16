using Ecommerce.Application.Carts.Interfaces;
using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Carts.Commands.RemoveItem;

public class RemoveItemHandler : IRequestHandler<RemoveItemCommand>
{
    private readonly ICartRepository _cartRepo;
    private readonly IUnitOfWork _uow;

    public RemoveItemHandler(ICartRepository cartRepo, IUnitOfWork uow)
    {
        _cartRepo = cartRepo;
        _uow = uow;
    }

    public async Task Handle(RemoveItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepo.GetByUserIdAsync(request.UserId);
        if (cart == null) return;

        cart.RemoveItem(request.ProductId);
        
        await _uow.SaveChangesAsync();
    }
}
