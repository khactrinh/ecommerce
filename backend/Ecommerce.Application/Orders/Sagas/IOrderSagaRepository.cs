namespace Ecommerce.Application.Orders.Sagas;

public interface IOrderSagaRepository
{
    Task<OrderSagaState?> GetAsync(Guid orderId);
    Task SaveAsync(OrderSagaState state);
}