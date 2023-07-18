using FluentResults;
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

    public CreateGameCommandHandler(IUnitOfWork unitOfWork, IGameRepository gameRepository)
    {
        _unitOfWork = unitOfWork;
        _gameRepo = gameRepository;
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

        return game;
    }
}
