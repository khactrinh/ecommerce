using System.Security.Claims;

namespace Ecommerce.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            user.FindFirst("sub")?.Value ??
            user.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException("UserId not found in token");

        return Guid.Parse(value);
    }
}