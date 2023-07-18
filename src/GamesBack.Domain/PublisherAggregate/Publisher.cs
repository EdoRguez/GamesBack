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
        string name,
        List<Game> games
    )
    {
        Update(
            name,
            games
        );
    }

    public static Publisher Create(
        string name,
        List<Game> games
    )
    {
        Publisher publisher = new Publisher(
            PublisherId.CreateUnique(),
            name,
            games
        );

        return publisher;
    }

    public void Update(
        string name,
        List<Game> games
    )
    {
        Name = name;
        _games = games;
    }


    public string Name { get; private set; } = string.Empty;
    private List<Game> _games = new();
    public IReadOnlyList<Game> Games => _games.AsReadOnly();
}