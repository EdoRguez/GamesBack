using FluentResults;
using GamesBack.Application.Common.Constants;
using GamesBack.Application.Common.Errors;
using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Commands.DeleteGame;

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Result<Game>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGameRepository _gameRepo;
    private readonly ICacheService _cacheService;

    public DeleteGameCommandHandler(IUnitOfWork unitOfWork, IGameRepository gameRepo, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _gameRepo = gameRepo;
        _cacheService = cacheService;
    }

    public async Task<Result<Game>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepo.Get(request.Id);

        if(game is null)
            return Result.Fail<Game>(new[] { new NotFoundError() {} });

        _gameRepo.Delete(game);
        await _unitOfWork.SaveChangesAsync();

        await _cacheService.RemoveAsync(CacheConstants.Games);

        return Result.Ok();
    }
}
