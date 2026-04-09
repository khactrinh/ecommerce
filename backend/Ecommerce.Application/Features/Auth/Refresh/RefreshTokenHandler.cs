using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Auth.Login;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Identity;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Refresh;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
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

    public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var token = await _repo.GetByTokenAsync(request.Token);

        if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Token is invalid or expired.");
        
        if (token.IsRevoked)
            throw new UnauthorizedAccessException("Token revoked");

        var user = await _userRepo.GetByIdAsync(token.UserId);
        
        if (user == null)
            throw new UnauthorizedAccessException("User not found.");

        token.IsRevoked = true;

        var newRefresh = _jwtService.GenerateRefreshToken();

        await _repo.AddAsync(new RefreshToken
        {
            UserId = user.Id,
            Token = newRefresh,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedByIp = request.IpAddress
        });

        await _uow.SaveChangesAsync(ct);

        var access = _jwtService.GenerateAccessToken(user);

        return new LoginResponse
        {
            AccessToken = access,
            RefreshToken = newRefresh
        };
    }
}