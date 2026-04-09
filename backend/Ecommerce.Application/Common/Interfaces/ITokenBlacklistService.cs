namespace Ecommerce.Application.Common.Interfaces;

public interface ITokenBlacklistService
{
    Task BlacklistTokenAsync(string jti, DateTime expiry);
    Task<bool> IsBlacklistedAsync(string jti);
}