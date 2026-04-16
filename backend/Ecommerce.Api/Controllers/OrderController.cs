using Ecommerce.Application.Orders.Commands.Checkout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

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
    public async Task<IActionResult> Checkout(Guid userId)
    {
        var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                 ?? HttpContext.Connection.RemoteIpAddress?.ToString();

        var command = new CheckoutCommand(userId, ip!);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}