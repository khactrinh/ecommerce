using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Application.Common.Caching;

public static class CacheExtensions
{
    public static async Task<T> GetOrSetAsync<T>(
        this ICacheService cache,
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiry = null)
    {
        var cached = await cache.GetAsync<T>(key);

        if (cached is not null)
            return cached;

        var result = await factory();

        await cache.SetAsync(key, result, expiry);

        return result;
    }
}