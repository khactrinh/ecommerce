using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Application.Common.Caching;

// Wrapper to properly detect cache miss for value types (e.g. ValueTuple)
internal class CacheWrapper<T>
{
    public T Value { get; set; } = default!;
}

public static class CacheExtensions
{
    public static async Task<T> GetOrSetAsync<T>(
        this ICacheService cache,
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiry = null)
    {
        // Use a wrapper so that value types (structs/tuples) are properly
        // detected as null when the cache key doesn't exist.
        var cached = await cache.GetAsync<CacheWrapper<T>>(key);

        if (cached is not null)
            return cached.Value;

        var result = await factory();

        await cache.SetAsync(key, new CacheWrapper<T> { Value = result }, expiry);

        return result;
    }
}