using FluentResults;
using GamesBack.Application.Common.Constants;
using GamesBack.Application.Common.Includes;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Queries.GetAllGame;

public class GetAllGameQueryHandler : IRequestHandler<GetAllGameQuery, Result<IEnumerable<Game>>>
{
    private readonly IGameRepository _gameRepo;
    private readonly ICacheService _cacheService;

    public GetAllGameQueryHandler(IGameRepository gameRepo, ICacheService cacheService)
    {
        _gameRepo = gameRepo;
        _cacheService = cacheService;
    }

    public async Task<Result<IEnumerable<Game>>> Handle(GetAllGameQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Game>? cacheGames = await _cacheService.GetAsync<IEnumerable<Game>>(CacheConstants.Games, cancellationToken);

        if(cacheGames is not null)
            return Result.Ok(cacheGames);

        string[] includes = { GameIncludes.Reviews, GameIncludes.Publishers };

        var games = await _gameRepo.GetAll(false, includes);

        await _cacheService.SetAsync(CacheConstants.Games, games, cancellationToken);

        return Result.Ok(games);
    }
}
