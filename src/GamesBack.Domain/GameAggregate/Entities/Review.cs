using GamesBack.Domain.Common.Models;
using GamesBack.Domain.GameAggregate.ValueObjects;

namespace GamesBack.Domain.GameAggregate.Entities;

public sealed class Review : Entity<ReviewId>
{
    private Review() {}

    private Review(ReviewId id, string description, byte rating)
        : base(id)
    {
        Description = description;
        Rating = rating;
    }

    public static Review Create(string description, byte rating)
    {
        rating = ValidateRating(rating);
        
        return new Review(
            ReviewId.CreateUnique(),
            description,
            rating
        );
    }

    public static Review Create(ReviewId reviewId, string description, byte rating)
    {
        rating = ValidateRating(rating);

        return new Review(
            reviewId,
            description,
            rating
        );
    }

    public string Description { get; private set; } = string.Empty;
    public byte Rating { get; private set; }

    private static byte ValidateRating(byte rating)
    {
        if(rating > 10)
            rating = 10;
        else if(rating < 1)
            rating = 1;

        return rating;
    }
}