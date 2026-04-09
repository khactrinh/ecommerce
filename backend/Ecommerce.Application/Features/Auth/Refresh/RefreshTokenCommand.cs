using MediatR;
using Ecommerce.Application.Features.Auth.Login;

namespace Ecommerce.Application.Features.Auth.Refresh;

public record RefreshTokenCommand(string Token, string IpAddress)
    : IRequest<LoginResponse>;