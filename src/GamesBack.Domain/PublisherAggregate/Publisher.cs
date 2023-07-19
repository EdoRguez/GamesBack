using GamesBack.Domain.Common.Models;
using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.PublisherAggregate.ValueObjects;

namespace GamesBack.Domain.PublisherAggregate;

public sealed class Publisher : AggregateRoot<PublisherId>
{
    private Publisher()
    {
    }

    private Publisher(
        PublisherId publisherId,
        string name
    ) : base(publisherId)
    {
        Update(
            name
        );
    }

    public static Publisher Create(
        string name
    )
    {
        Publisher publisher = new Publisher(
            PublisherId.CreateUnique(),
            name
        );

        return publisher;
    }

    public static Publisher Create(
        Guid publisherId,
        string name
    )
    {
        Publisher publisher = new Publisher(
            PublisherId.Create(publisherId),
            name
        );

        return publisher;
    }

    public void Update(
        string name
    )
    {
        Name = name;
    }


    public string Name { get; private set; } = string.Empty;
    private List<Game> _games = new();
    public IReadOnlyList<Game> Games => _games.AsReadOnly();
}