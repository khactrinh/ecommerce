using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _uow;
    private readonly IPaymentService _paymentService;

    public PaymentController(
        IOrderRepository orderRepo,
        IUnitOfWork uow,
        IPaymentService paymentService)
    {
        _orderRepo = orderRepo;
        _uow = uow;
        _paymentService = paymentService;
    }

    [HttpGet("vnpay-return")]
    public async Task<IActionResult> VnPayReturn()
    {
        var queryDict = Request.Query.ToDictionary(
            x => x.Key,
            x => x.Value.ToString()
        );

        var isValid = _paymentService.VerifyPayment(queryDict);

        if (!isValid)
            return BadRequest("Invalid signature");

        if (!Guid.TryParse(Request.Query["vnp_TxnRef"], out var orderId))
            return BadRequest("Invalid orderId");

        var responseCode = Request.Query["vnp_ResponseCode"];

        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null)
            return NotFound("Order not found");

        // 🔥 Idempotency
        if (order.PaymentStatus == PaymentStatus.Paid)
        {
            return Redirect($"http://localhost:3000/payment-result?success=true");
        }

        if (responseCode == "00")
            order.MarkAsPaid();
        else
            order.FailPayment();

        await _uow.SaveChangesAsync();

        return Redirect(
            $"http://localhost:3000/payment-result?success={responseCode == "00"}"
        );
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment(Guid orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);

        if (order == null)
            return NotFound("Order not found");

        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        var paymentUrl = _paymentService.CreatePaymentUrl(order, ip);

        return Ok(paymentUrl); // 🔥 FE nhận URL luôn
    }
}