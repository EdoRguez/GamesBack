using GamesBack.Domain.Common.Models;
using GamesBack.Domain.GameAggregate.Entities;
using GamesBack.Domain.GameAggregate.ValueObjects;
using GamesBack.Domain.PublisherAggregate;
using GamesBack.Domain.PublisherAggregate.ValueObjects;

namespace GamesBack.Domain.GameAggregate;

public sealed class Game : AggregateRoot<GameId>
{
    private Game()
    {
    }

    private Game(
        GameId gameId,
        string name,
        List<Review> reviews,
        PublisherId publisherId
    ) : base(gameId)
    {
       Update(
        name,
        reviews,
        publisherId
       );
    }

    public static Game Create(
        string name,
        List<Review> reviews,
        PublisherId publisherId
    )
    {
        Game game = new Game(
            GameId.CreateUnique(),
            name,
            reviews,
            publisherId
        );

        return game;
    }

    public void Update(
        string name,
        List<Review> reviews,
        PublisherId publisherId
    )
    {
        Name = name;
        _reviews = reviews;
        PublisherId = publisherId;
    }

    private List<Review> _reviews = new();
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();
    public string Name { get; private set; } = string.Empty;
    public PublisherId PublisherId { get; private set; } = null!;
    public Publisher Publisher { get; private set; } = null!;
}