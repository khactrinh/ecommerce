using MediatR;

namespace Ecommerce.Application.Features.Auth.Logout;

public record LogoutCommand(string RefreshToken, string IpAddress, string JwtToken)
    : IRequest<Unit>;