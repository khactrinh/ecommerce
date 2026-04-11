namespace Ecommerce.Domain.Enums;

public enum OrderStatus
{
    Pending,
    PaymentProcessing,
    Paid,
    Failed,
    Cancelled,
    Shipped,
    Completed
}