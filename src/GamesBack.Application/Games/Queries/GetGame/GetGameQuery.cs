using FluentResults;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Queries.GetGame;

public record GetGameQuery(Guid Id) : IRequest<Result<Game>>;