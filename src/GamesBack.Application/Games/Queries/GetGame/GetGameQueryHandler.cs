using FluentResults;
using GamesBack.Application.Common.Errors;
using GamesBack.Application.Common.Includes;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Queries.GetGame;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, Result<Game>>
{
    private readonly IGameRepository _gameRepo;

    public GetGameQueryHandler(IGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public async Task<Result<Game>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        string[] includes = { GameIncludes.Reviews, GameIncludes.Publishers };

        var game = await _gameRepo.Get(request.Id, false, includes);

        if(game is null)
            return Result.Fail<Game>(new[] { new NotFoundError() });

        return Result.Ok(game);
    }
}
