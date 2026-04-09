using Ecommerce.Application.Features.Auth.Login;
using MediatR;

public record LoginCommand(string Email, string Password, string IpAddress)
    : IRequest<LoginResponse>;