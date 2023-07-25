namespace GamesBack.Contracts.Games;

public record UpdateGameRequest(
    string Id,
    string Name,
    List<UpdateReviewRequest> Reviews,
    string PublisherId
);

public record UpdateReviewRequest(
    string Id,
    string Description,
    byte Rating
);