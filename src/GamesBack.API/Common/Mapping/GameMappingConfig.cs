using GamesBack.Application.Games.Commands.CreateGame;
using GamesBack.Application.Games.Commands.UpdateGame;
using GamesBack.Contracts.Games;
using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.GameAggregate.Entities;
using Mapster;

namespace GamesBack.API.Common.Mapping;

public class GameMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Game, GameResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<Review, ReviewResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<CreateGameRequest, CreateGameCommand>();

        config.NewConfig<UpdateGameRequest, UpdateGameCommand>();
    }
}