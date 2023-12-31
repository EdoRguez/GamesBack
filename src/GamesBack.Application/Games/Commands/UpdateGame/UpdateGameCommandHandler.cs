using FluentResults;
using GamesBack.Application.Common.Constants;
using GamesBack.Application.Common.Errors;
using GamesBack.Application.Common.Includes;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.GameAggregate.Entities;
using GamesBack.Domain.PublisherAggregate.ValueObjects;
using MediatR;

namespace GamesBack.Application.Games.Commands.UpdateGame;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Result<Game>>
{
    private readonly IGameRepository _gameRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public UpdateGameCommandHandler(IGameRepository gameRepository, IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _gameRepo = gameRepository;
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<Result<Game>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        string[] includes = { GameIncludes.Reviews, GameIncludes.Publishers };

        var game = await _gameRepo.Get(Guid.Parse(request.Id), true, includes);

        if(game is null)
            return Result.Fail<Game>(new[] { new NotFoundError() });

        game.Update(
            request.Name,
            request.Reviews.ConvertAll(x => Review.Create(x.Description, x.Rating)),
            PublisherId.Create(Guid.Parse(request.PublisherId))
        );

        await _unitOfWork.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheConstants.Games);

        return Result.Ok();
    }
}
