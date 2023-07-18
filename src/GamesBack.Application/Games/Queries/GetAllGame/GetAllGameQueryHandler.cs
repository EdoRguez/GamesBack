using FluentResults;
using GamesBack.Application.Common.Includes;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Queries.GetAllGame;

public class GetAllGameQueryHandler : IRequestHandler<GetAllGameQuery, Result<IEnumerable<Game>>>
{
    private readonly IGameRepository _gameRepo;

    public GetAllGameQueryHandler(IGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public async Task<Result<IEnumerable<Game>>> Handle(GetAllGameQuery request, CancellationToken cancellationToken)
    {
        string[] includes = { GameIncludes.Review, GameIncludes.Publisher };

        var games = await _gameRepo.GetAll(false, includes);

        return Result.Ok(games);
    }
}
