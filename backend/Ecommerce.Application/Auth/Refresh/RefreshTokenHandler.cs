using Ecommerce.Application.Auth.Login;
using Ecommerce.Application.Auth.Refresh;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Refresh;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _repo;
    private readonly IUserRepository _userRepo;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _uow;

    public RefreshTokenHandler(
        IRefreshTokenRepository repo,
        IUserRepository userRepo,
        IJwtService jwtService,
        IUnitOfWork uow)
    {
        _repo = repo;
        _userRepo = userRepo;
        _jwtService = jwtService;
        _uow = uow;
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var token = await _repo.GetByTokenAsync(request.Token);

        if (token == null)
            throw new UnauthorizedAccessException("Invalid token");

        // =========================
        // ❗ REUSE DETECTION FIX
        // =========================
        if (token.IsUsed)
        {
            await _repo.RevokeFamilyAsync(token.FamilyId, request.IpAddress);
            throw new UnauthorizedAccessException("Refresh token reuse detected (BREACH)");
        }

        if (token.IsRevoked)
            throw new UnauthorizedAccessException("Token already revoked");

        if (token.ExpiryDate < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Token expired");

        var user = await _userRepo.GetByIdAsync(token.UserId);

        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        // =========================
        // MARK AS USED (IMPORTANT)
        // =========================
        token.IsUsed = true;
        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        token.RevokedByIp = request.IpAddress;

        var newRefresh = _jwtService.GenerateRefreshToken();

        await _repo.AddAsync(new RefreshToken
        {
            UserId = user.Id,
            Token = newRefresh,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedByIp = request.IpAddress,
            ParentTokenId = token.Id,
            FamilyId = token.FamilyId,
            IsUsed = false,
            IsRevoked = false
        });

        await _uow.SaveChangesAsync(ct);

        var access = _jwtService.GenerateAccessToken(user);

        return new RefreshTokenResponse
        {
            AccessToken = access,
            RefreshToken = newRefresh
        };
    }
}