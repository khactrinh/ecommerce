namespace Ecommerce.Application.Orders.Sagas;

public class OrderSagaState
{
    public Guid OrderId { get; set; }

    public bool PaymentCompleted { get; set; }
    public bool InventoryReserved { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}