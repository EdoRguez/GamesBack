using Carter;
using GamesBack.Application.Games.Commands.CreateGame;
using GamesBack.Contracts.Games;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamesBack.API.Endpoints;

public class Games : CarterModule
{
    public Games()
        : base("/api/games")
    {

    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender) =>
        {
            return Results.Ok();
        });

        app.MapPost("/", async ([FromServices] ISender sender, [FromServices] IMapper mapper, [FromBody] CreateGameRequest request) =>
        {
            var command = mapper.Map<CreateGameCommand>(request);

            var createGameResult = await sender.Send(command);

            return Results.Ok(createGameResult);
        });
    }
}