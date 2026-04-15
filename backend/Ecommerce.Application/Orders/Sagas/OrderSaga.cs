using Ecommerce.Application.Common.Messaging;
using Ecommerce.Domain.Orders.Events;

namespace Ecommerce.Application.Orders.Sagas;

public class OrderSaga
{
    private readonly IOrderSagaRepository _repo;
    private readonly IRabbitMqPublisher _publisher;

    public OrderSaga(
        IOrderSagaRepository repo,
        IRabbitMqPublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    // STEP 1
    public async Task Handle(OrderCreatedEvent evt)
    {
        var saga = new OrderSagaState
        {
            OrderId = evt.OrderId
        };

        await _repo.SaveAsync(saga);

        await _publisher.PublishAsync(
            "PaymentRequestedEvent",
            new { evt.OrderId, evt.Amount }
        );
    }

    // STEP 2
    public async Task Handle(PaymentSucceededEvent evt)
    {
        var saga = await _repo.GetAsync(evt.OrderId)!;

        saga.PaymentCompleted = true;

        await _repo.SaveAsync(saga);

        await _publisher.PublishAsync(
            "ReserveInventoryEvent",
            new { evt.OrderId }
        );
    }

    // FAIL PAYMENT
    public async Task Handle(PaymentFailedEvent evt)
    {
        var saga = await _repo.GetAsync(evt.OrderId)!;

        saga.Status = "Cancelled";

        await _repo.SaveAsync(saga);

        await _publisher.PublishAsync(
            "OrderCancelledEvent",
            new { evt.OrderId }
        );
    }

    // STEP 3
    public async Task Handle(InventoryReservedEvent evt)
    {
        var saga = await _repo.GetAsync(evt.OrderId)!;

        saga.InventoryReserved = true;
        saga.Status = "Completed";

        await _repo.SaveAsync(saga);

        await _publisher.PublishAsync(
            "OrderCompletedEvent",
            new { evt.OrderId }
        );
    }

    // FAIL INVENTORY
    public async Task Handle(InventoryFailedEvent evt)
    {
        var saga = await _repo.GetAsync(evt.OrderId)!;

        saga.Status = "Cancelled";

        await _repo.SaveAsync(saga);

        await _publisher.PublishAsync("RefundPaymentEvent", new { evt.OrderId });
        await _publisher.PublishAsync("OrderCancelledEvent", new { evt.OrderId });
    }
}