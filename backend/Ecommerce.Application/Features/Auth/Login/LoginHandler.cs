using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Identity;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepo;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshRepo;
    private readonly IUnitOfWork _uow;

    public LoginHandler(
        IUserRepository userRepo,
        IJwtService jwtService,
        IRefreshTokenRepository refreshRepo,
        IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _jwtService = jwtService;
        _refreshRepo = refreshRepo;
        _uow = uow;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _userRepo.GetByEmailAsync(request.Email);

        if (user == null || !user.VerifyPassword(request.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _refreshRepo.AddAsync(new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedByIp = request.IpAddress,
            FamilyId = Guid.NewGuid()
        });

        await _uow.SaveChangesAsync(ct);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}