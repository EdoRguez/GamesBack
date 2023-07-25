using Carter;
using GamesBack.Application.Common.Errors;
using GamesBack.Application.Games.Commands.CreateGame;
using GamesBack.Application.Games.Commands.DeleteGame;
using GamesBack.Application.Games.Commands.UpdateGame;
using GamesBack.Application.Games.Queries.GetAllGame;
using GamesBack.Application.Games.Queries.GetGame;
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
        app.MapGet("/", async ([FromServices] ISender sender, [FromServices] IMapper mapper) =>
        {
           var games = await sender.Send(new GetAllGameQuery());

           var gamesResponse = mapper.Map<IEnumerable<GameResponse>>(games.Value);

            return Results.Ok(gamesResponse);
        });

        app.MapGet("/{id}", async ([FromServices] ISender sender, [FromServices] IMapper mapper, [FromRoute] Guid id) =>
        {
           var game = await sender.Send(new GetGameQuery(id));

            if(game.IsFailed)
            {
                var firstError = game.Errors[0];

                if(firstError is NotFoundError)
                    return Results.Problem(statusCode: (int) StatusCodes.Status404NotFound, detail: firstError.Message);

                return Results.Problem(statusCode: (int) StatusCodes.Status409Conflict, detail: firstError.Message);
            }

           var gameResponse = mapper.Map<GameResponse>(game.Value);

            return Results.Ok(gameResponse);
        });

        app.MapPost("/", async ([FromServices] ISender sender, [FromServices] IMapper mapper, [FromBody] CreateGameRequest request) =>
        {
            var command = mapper.Map<CreateGameCommand>(request);

            var createGameResult = await sender.Send(command);

            if(createGameResult.IsFailed)
            {
                var firstError = createGameResult.Errors[0];

                return Results.Problem(statusCode: (int) StatusCodes.Status409Conflict, detail: firstError.Message);
            }

            var gameResponse = mapper.Map<GameResponse>(createGameResult.Value);

            return Results.Ok(gameResponse);
        });

        app.MapPut("/{id}", async ([FromServices] ISender sender, [FromServices] IMapper mapper, [FromRoute] string id, [FromBody] UpdateGameRequest request) =>
        {
            if(id != request.Id)
                return Results.BadRequest();

            var command = mapper.Map<UpdateGameCommand>(request);

            var updateGameResult = await sender.Send(command);

            if(updateGameResult.IsFailed)
            {
                var firstError = updateGameResult.Errors[0];

                if(firstError is NotFoundError)
                    return Results.Problem(statusCode: (int) StatusCodes.Status404NotFound, detail: firstError.Message);

                return Results.Problem(statusCode: (int) StatusCodes.Status409Conflict, detail: firstError.Message);
            }

            return Results.NoContent();
        });

        app.MapDelete("/{id}", async ([FromServices] ISender sender, [FromRoute] Guid id) =>
        {
            var deleteGameResult = await sender.Send(new DeleteGameCommand(id));

            if(deleteGameResult.IsFailed)
            {
                var firstError = deleteGameResult.Errors[0];

                if(firstError is NotFoundError)
                    return Results.Problem(statusCode: (int) StatusCodes.Status404NotFound, detail: firstError.Message);

                return Results.Problem(statusCode: (int) StatusCodes.Status409Conflict, detail: firstError.Message);
            }

            return Results.NoContent();
        });
    }
}