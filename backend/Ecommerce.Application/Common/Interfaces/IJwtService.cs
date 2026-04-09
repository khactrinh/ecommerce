using Ecommerce.Domain.Identity;

namespace Ecommerce.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}