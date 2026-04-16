using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    
    Task<Order?> GetByIdAsync(Guid id);
}