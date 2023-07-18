using FluentResults;
using GamesBack.Domain.GameAggregate;
using MediatR;

namespace GamesBack.Application.Games.Queries.GetAllGame;

public record GetAllGameQuery() : IRequest<Result<IEnumerable<Game>>>;