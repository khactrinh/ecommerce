using Ecommerce.Application.Common.Interfaces;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure.Identity;

public class RedisTokenBlacklistService : ITokenBlacklistService
{
    private readonly IDatabase _db;

    public RedisTokenBlacklistService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task BlacklistTokenAsync(string jti, DateTime expiry)
    {
        if (string.IsNullOrWhiteSpace(jti))
            throw new ArgumentException("jti cannot be null");

        var ttl = expiry - DateTime.UtcNow;

        if (ttl <= TimeSpan.Zero)
            return;

        // buffer chống clock skew
        ttl = ttl.Add(TimeSpan.FromSeconds(30));

        try
        {
            await _db.StringSetAsync(
                key: $"blacklist:v1:{jti}",
                value: "1",
                expiry: ttl
            );
        }
        catch (Exception ex)
        {
            // TODO: log (Serilog, etc.)
        }
    }

    public async Task<bool> IsBlacklistedAsync(string jti)
    {
        if (string.IsNullOrWhiteSpace(jti))
            return false;

        var value = await _db.StringGetAsync($"blacklist:v1:{jti}");
        return value.HasValue;
    }
}