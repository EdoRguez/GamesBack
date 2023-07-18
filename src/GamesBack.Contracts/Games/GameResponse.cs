using GamesBack.Contracts.Publisher;

namespace GamesBack.Contracts.Games;

public record GameResponse(
    string Id,
    string Name,
    List<ReviewResponse> Reviews,
    PublisherResponse Publisher
);

public record ReviewResponse(
    string Id,
    string Description,
    byte Rating
);