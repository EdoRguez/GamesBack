using GamesBack.Domain.Common.Models;

namespace GamesBack.Domain.GameAggregate.ValueObjects;

public sealed class GameId : ValueObject
{
    public Guid Value { get; }

    public GameId(Guid value)
    {
        Value = value;
    }

    public static GameId CreateUnique()
    {
        return new GameId(Guid.NewGuid());
    }

    public static GameId Create(Guid value)
    {
        return new GameId(value);
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}