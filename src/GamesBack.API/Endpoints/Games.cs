using Carter;
using MediatR;

namespace GamesBack.API.Endpoints;

public class Games : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games", async (ISender sender) =>
        {
            return Results.Ok();
        });
    }
}