namespace Ecommerce.Application.Carts.Interfaces;

public interface ICartRepository
{
    Task<Domain.Cart.Cart?> GetByUserIdAsync(Guid userId);

    Task ClearAsync(Guid userId);

    Task Add(Domain.Cart.Cart cart);

}