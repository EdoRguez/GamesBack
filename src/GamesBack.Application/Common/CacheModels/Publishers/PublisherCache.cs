namespace GamesBack.Application.Common.CacheModels.Publishers;

public class PublisherCacheId
{
    public Guid Value { get; private set; }
}

public class PublisherCache
{
    public PublisherCacheId Id { get;  set; } = null!;
    public string Name { get;  set; } = null!;
}