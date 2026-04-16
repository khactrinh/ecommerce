using Ecommerce.Application.Carts.Interfaces;
using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Carts.Commands.UpdateQuantity;

public class UpdateQuantityHandler : IRequestHandler<UpdateQuantityCommand>
{
    private readonly ICartRepository _cartRepo;
    private readonly IUnitOfWork _uow;

    public UpdateQuantityHandler(ICartRepository cartRepo, IUnitOfWork uow)
    {
        _cartRepo = cartRepo;
        _uow = uow;
    }

    public async Task Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepo.GetByUserIdAsync(request.UserId);
        if (cart == null) return;

        cart.UpdateQuantity(request.ProductId, request.Quantity);
        
        await _uow.SaveChangesAsync();
    }
}
