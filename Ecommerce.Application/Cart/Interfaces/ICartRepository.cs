namespace Ecommerce.Application.Cart.Interfaces;

public interface ICartRepository
{
    Task<Domain.Cart.Cart?> GetByUserIdAsync(Guid userId);

    Task ClearAsync(Guid userId);
}