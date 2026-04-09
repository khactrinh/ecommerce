using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Features.Auth.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, Unit>
{
    private readonly IRefreshTokenRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly ITokenBlacklistService _blacklist;

    public LogoutHandler(IRefreshTokenRepository repo,  IUnitOfWork uow,  ITokenBlacklistService blacklist)
    {
        _repo = repo;
        _uow = uow;
        _blacklist = blacklist;
    }

    public async Task<Unit> Handle(LogoutCommand request, CancellationToken ct)
    {
        var token = await _repo.GetByTokenAsync(request.RefreshToken);

        if (token == null || token.IsRevoked)
            return Unit.Value;

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        token.RevokedByIp = request.IpAddress;
        
        await _uow.SaveChangesAsync(ct);
        
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(request.JwtToken);

        var jti = jwt.Id;
        var exp = jwt.ValidTo;
        await _blacklist.BlacklistTokenAsync(jti, exp);

        return Unit.Value;
    }
}