namespace Ecommerce.Application.Orders.Commands.Checkout;

public class CheckoutResponse
{
    public Guid OrderId { get; set; }
    public string PaymentUrl { get; set; }
}