using Ecommerce.Application.Carts.Interfaces;
using Ecommerce.Domain.Cart;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task Add(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
    }

    public async Task ClearAsync(Guid userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null) return;

        _context.CartItems.RemoveRange(cart.Items);
        _context.Carts.Remove(cart);
    }
}