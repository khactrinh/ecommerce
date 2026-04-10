using Ecommerce.Domain.Identity;

namespace Ecommerce.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<List<RefreshToken>> GetByUserIdAsync(Guid userId);
    
    Task RevokeFamilyAsync(Guid familyId, string ipAddress);
}