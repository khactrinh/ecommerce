using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Logout;

public class RevokeAllTokensHandler 
    : IRequestHandler<RevokeAllTokensCommand, Unit>
{
    private readonly IRefreshTokenRepository _repo;

    public async Task<Unit> Handle(RevokeAllTokensCommand request, CancellationToken ct)
    {
        var tokens = await _repo.GetByUserIdAsync(request.UserId);

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = request.IpAddress;
        }

        return Unit.Value;
    }
}