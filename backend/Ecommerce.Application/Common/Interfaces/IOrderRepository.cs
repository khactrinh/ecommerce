using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}