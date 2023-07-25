using FluentResults;
using GamesBack.Application.Games.Commands.CreateGame;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Commands.UpdateGame;

public record UpdateGameCommand(
    string Id,
    string Name,
    List<CreateReviewCommand> Reviews,
    string PublisherId
) : IRequest<Result<Game>>;

public record UpdateReviewCommand(
    string Id,
    string Description,
    byte Rating
);