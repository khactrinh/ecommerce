using MediatR;

namespace Ecommerce.Application.Features.Auth.Logout;

public record RevokeAllTokensCommand(Guid UserId, string IpAddress)
    : IRequest<Unit>;