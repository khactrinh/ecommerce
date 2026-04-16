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

    public PaymentController(IOrderRepository orderRepo, IUnitOfWork uow, IPaymentService paymentService)
    {
        _orderRepo = orderRepo;
        _uow = uow;
        _paymentService = paymentService;
    }

    [HttpGet("vnpay-return")]
    public async Task<IActionResult> VnPayReturn()
    {
        var query = Request.Query;

        // ✅ 1. Verify chữ ký (CRITICAL)
        var queryDict = Request.Query.ToDictionary(
            x => x.Key,
            x => x.Value.ToString()
        );
        var isValid = _paymentService.VerifyPayment(queryDict);

        if (!isValid)
        {
            return BadRequest("Invalid signature");
        }

        // ✅ 2. Parse dữ liệu
        if (!Guid.TryParse(query["vnp_TxnRef"], out var orderId))
            return BadRequest("Invalid orderId");

        var responseCode = query["vnp_ResponseCode"];

        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null)
            return NotFound();

        // ✅ 3. Idempotency (tránh xử lý 2 lần)
        if (order.PaymentStatus == PaymentStatus.Paid)
        {
            return Redirect($"http://localhost:3000/payment-result?success=true");
        }

        // ✅ 4. Update trạng thái
        if (responseCode == "00")
        {
            order.MarkAsPaid();
        }
        else
        {
            order.FailPayment();
        }

        await _uow.SaveChangesAsync();

        return Redirect($"http://localhost:3000/payment-result?success={responseCode == "00"}");
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment(Guid orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null)
            return NotFound();

        // ✅ LẤY IP Ở ĐÂY
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        // ✅ TRUYỀN VÀO SERVICE
        var paymentUrl = _paymentService.CreatePaymentUrl(order, ip);

        return Ok(paymentUrl);
    }
}