using FluentResults;
using GamesBack.Application.Common.Constants;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.GameAggregate.Entities;
using GamesBack.Domain.PublisherAggregate.ValueObjects;
using MediatR;

namespace GamesBack.Application.Games.Commands.CreateGame;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Result<Game>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGameRepository _gameRepo;
    private readonly ICacheService _cacheService;

    public CreateGameCommandHandler(IUnitOfWork unitOfWork, IGameRepository gameRepository, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _gameRepo = gameRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<Game>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = Game.Create(
            request.Name,
            request.Reviews.ConvertAll(x => Review.Create(x.Description, x.Rating)),
            PublisherId.Create(Guid.Parse(request.PublisherId))
        );

        _gameRepo.Add(game);
        await _unitOfWork.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheConstants.Games);

        return game;
    }
}
