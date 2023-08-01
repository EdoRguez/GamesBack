using System.Collections.Concurrent;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Infrastructure.Common.Resolvers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

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

        T? value = JsonConvert.DeserializeObject<T>(cachedValue, new JsonSerializerSettings()
        {
            // Configuration for private setters properties
            ContractResolver = new PrivatePropertyResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        });

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
        string cacheValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }
}