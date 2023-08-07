namespace GamesBack.Application.Common.CacheModels.Publishers;

public record PublisherCacheId(Guid Value);

public record PublisherCache(
    PublisherCacheId Id,
    string Name);