using Ecommerce.Application.Common.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redis;

    private const string PREFIX = "ecommerce";
    private const string VERSION = "v1";

    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    private static string BuildKey(string key)
        => $"{PREFIX}:{VERSION}:{key}";

    public async Task<T?> GetAsync<T>(string key)
    {
        var fullKey = BuildKey(key);

        var value = await _db.StringGetAsync(fullKey);

        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString(), _options);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var fullKey = BuildKey(key);

        var json = JsonSerializer.Serialize(value, _options);

        await _db.StringSetAsync(
            fullKey,
            json,
            expiry ?? TimeSpan.FromMinutes(5)
        );
    }

    public async Task RemoveAsync(string key)
    {
        var fullKey = BuildKey(key);
        await _db.KeyDeleteAsync(fullKey);
    }

    // ⚠️ Production basic version (scan keys)
    public async Task RemoveByPrefixAsync(string prefix)
    {
        var server = _redis.GetServer(_redis.GetEndPoints().First());

        var pattern = $"{PREFIX}:{VERSION}:{prefix}*";

        var keys = server.Keys(pattern: pattern).ToArray();

        if (keys.Length > 0)
        {
            await _db.KeyDeleteAsync(keys);
        }
    }
}