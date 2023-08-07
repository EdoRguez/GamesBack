using GamesBack.Application.Common.CacheModels.Publishers;

namespace GamesBack.Application.Common.CacheModels.Game;

public record GameCacheId(Guid Value);

public record GameCache(
    GameCacheId Id,
    string Name,
    PublisherCacheId PublisherId,
    PublisherCache Publisher,
    List<ReviewCache> Reviews);

public record ReviewCacheId(Guid Value);

public record ReviewCache(
    ReviewCacheId Id,
    string Description,
    byte Rating);