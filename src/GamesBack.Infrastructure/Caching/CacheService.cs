using System.Collections.Concurrent;
using System.Text.Json;
using GamesBack.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace GamesBack.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private static ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if(cachedValue is null)
            return null;

        T? value = JsonSerializer.Deserialize<T>(cachedValue);

        return value;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);

        CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(x => x.StartsWith(prefixKey))
            .Select(x => RemoveAsync(x, cancellationToken));

        await Task.WhenAll(tasks);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        string cacheValue = JsonSerializer.Serialize<T>(value);

        await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }
}