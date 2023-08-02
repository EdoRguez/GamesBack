using GamesBack.Application.Common.CacheModels.Publishers;

namespace GamesBack.Application.Common.CacheModels.Game;

public class GameCacheId
{
    public Guid Value { get; private set; }
}

public class GameCache
{
    public GameCacheId Id { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public PublisherCacheId PublisherId { get; private set; } = null!;
    public PublisherCache Publisher { get; private set; } = null!;
    public List<ReviewCache> Reviews { get; private set; } = null!;
}

public class ReviewCacheId
{
    public Guid Value { get; private set; }
}

public class ReviewCache
{
    public ReviewCacheId Id { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public byte Rating { get; private set; }
}