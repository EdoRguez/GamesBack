using GamesBack.Domain.Common.Models;

namespace GamesBack.Domain.PublisherAggregate.ValueObjects;

public sealed class PublisherId : ValueObject
{
    public Guid Value { get; }

    public PublisherId(Guid value)
    {
        Value = value;
    }

    public static PublisherId CreateUnique()
    {
        return new PublisherId(Guid.NewGuid());
    }

    public static PublisherId Create(Guid value)
    {
        return new PublisherId(value);
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}