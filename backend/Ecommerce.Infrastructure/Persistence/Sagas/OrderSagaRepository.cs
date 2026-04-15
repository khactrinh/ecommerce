using Ecommerce.Application.Orders.Sagas;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.Sagas;

public class OrderSagaRepository : IOrderSagaRepository
{
    private readonly AppDbContext _context;

    public OrderSagaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderSagaState?> GetAsync(Guid orderId)
    {
        return await _context.Set<OrderSagaState>()
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }

    public async Task SaveAsync(OrderSagaState state)
    {
        var existing = await _context.Set<OrderSagaState>()
            .FirstOrDefaultAsync(x => x.OrderId == state.OrderId);

        if (existing == null)
        {
            _context.Add(state);
        }
        else
        {
            existing.PaymentCompleted = state.PaymentCompleted;
            existing.InventoryReserved = state.InventoryReserved;
            existing.Status = state.Status;
        }

        await _context.SaveChangesAsync();
    }
}