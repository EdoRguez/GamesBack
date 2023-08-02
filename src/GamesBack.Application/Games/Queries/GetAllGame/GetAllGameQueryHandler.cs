using System.IO.Compression;
using FluentResults;
using GamesBack.Application.Common.CacheModels.Game;
using GamesBack.Application.Common.Constants;
using GamesBack.Application.Common.Includes;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.GameAggregate.Entities;
using GamesBack.Domain.GameAggregate.ValueObjects;
using GamesBack.Domain.PublisherAggregate;
using GamesBack.Domain.PublisherAggregate.ValueObjects;
using MediatR;
using System.Collections.Generic;

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
        List<GameCache>? gamesCache = await _cacheService.GetAsync<List<GameCache>>(CacheConstants.Games, cancellationToken);

        if(gamesCache is not null)
        {
            var gamesResult = gamesCache.ConvertAll(x =>
            {
                return Game.Create(
                    GameId.Create(x.Id.Value),
                    x.Name,
                    x.Reviews.ConvertAll(y =>
                        Review.Create(ReviewId.Create(y.Id.Value),
                                        y.Description, y.Rating)
                    ),
                    PublisherId.Create(x.PublisherId.Value),
                    Publisher.Create(x.Publisher.Id.Value, x.Publisher.Name)
                );
            })
            .AsEnumerable();

            return Result.Ok(gamesResult);
        }

        string[] includes = { GameIncludes.Reviews, GameIncludes.Publishers };

        var games = await _gameRepo.GetAll(false, includes);

        await _cacheService.SetAsync(CacheConstants.Games, games, cancellationToken);

        return Result.Ok(games);
    }
}
