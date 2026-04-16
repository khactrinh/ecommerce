using Ecommerce.Application.Auth.Login;
using Ecommerce.Application.Auth.Refresh;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Refresh;

public record RefreshTokenCommand(string Token, string IpAddress)
    : IRequest<RefreshTokenResponse>;