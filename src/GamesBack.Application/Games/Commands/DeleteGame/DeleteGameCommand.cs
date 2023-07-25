using FluentResults;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Commands.DeleteGame;

public record DeleteGameCommand(Guid Id) : IRequest<Result<Game>>;