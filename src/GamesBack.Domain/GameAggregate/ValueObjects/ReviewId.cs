using GamesBack.Domain.Common.Models;

namespace GamesBack.Domain.GameAggregate.ValueObjects;

public sealed class ReviewId : ValueObject
{
    public Guid Value { get; }

    public ReviewId(Guid value)
    {
        Value = value;
    }

    public static ReviewId CreateUnique()
    {
        return new ReviewId(Guid.NewGuid());
    }

    public static ReviewId Create(Guid value)
    {
        return new ReviewId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}