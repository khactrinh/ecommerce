using MediatR;

namespace  Ecommerce.Application.Auth.Login;
public record LoginCommand(string Email, string Password, string IpAddress)
    : IRequest<LoginResponse>;