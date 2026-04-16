using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Common.Interfaces;

public interface IPaymentService
{
    string CreatePaymentUrl(Order order, string ipAddress);
    bool VerifyPayment(Dictionary<string, string> query);
}