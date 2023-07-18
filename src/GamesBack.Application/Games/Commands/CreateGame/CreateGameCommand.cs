using FluentResults;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Commands.CreateGame;

public record CreateGameCommand(
    string Name,
    List<CreateReviewCommand> Reviews,
    string PublisherId
) : IRequest<Result<Game>>;

public record CreateReviewCommand(
    string Description,
    byte Rating
);