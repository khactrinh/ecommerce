using System.IdentityModel.Tokens.Jwt;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Api.Middleware;

public class JwtBlacklistMiddleware
{
    private readonly RequestDelegate _next;

    public JwtBlacklistMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        ITokenBlacklistService blacklist)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader?.StartsWith("Bearer ") == true)
        {
            var token = authHeader.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwt = handler.ReadJwtToken(token);
                var jti = jwt.Id;

                if (!string.IsNullOrEmpty(jti))
                {
                    var isBlacklisted = await blacklist.IsBlacklistedAsync(jti);

                    if (isBlacklisted)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                }
            }
        }

        await _next(context);
    }
}