using Ecommerce.Application.Auth.Login;
using Ecommerce.Application.Auth.Refresh;
using Ecommerce.Application.Features.Auth.Logout;
using Ecommerce.Application.Features.Auth.Refresh;
using Ecommerce.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        command = command with { IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() };

        var result = await _mediator.Send(command);
        return Ok(ApiResponse<LoginResponse>.Ok(result));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
    {
        command = command with { IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() };

        var result = await _mediator.Send(command);
        return Ok(ApiResponse<RefreshTokenResponse>.Ok(result));
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        var jwt = HttpContext.Request.Headers["Authorization"]
            .FirstOrDefault()?.Replace("Bearer ", "");
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var command = new LogoutCommand(refreshToken, ip, jwt);

        await _mediator.Send(command);
        return Ok(ApiResponse<string>.Ok("Logged out"));
    }
    
    [Authorize]
    [HttpPost("logout-all")]
    public async Task<IActionResult> LogoutAll()
    {
        var userId = Guid.Parse(User.FindFirst("sub")!.Value);

        var command = new RevokeAllTokensCommand(
            userId,
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        await _mediator.Send(command);
        return Ok(ApiResponse<string>.Ok("Logged out"));
    }
}