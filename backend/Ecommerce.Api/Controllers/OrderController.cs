using Ecommerce.Application.Orders.Commands.Checkout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);

        var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                 ?? HttpContext.Connection.RemoteIpAddress?.ToString();

        var command = new CheckoutCommand(userId, ip!);

        var result = await _mediator.Send(command);

        return Ok(result); // 🔥 clean
    }
}