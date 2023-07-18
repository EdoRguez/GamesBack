namespace GamesBack.Contracts.Games;

public record CreateGameRequest(
    string Name,
    List<CreateReviewRequest> Reviews,
    string PublisherId
);

public record CreateReviewRequest(
    string Description,
    byte Rating
);