using System.Text.Json.Serialization;
using Carter;
using GamesBack.API.Common.Mapping;
using Microsoft.AspNetCore.Http.Json;

namespace GamesBack.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCarter();

        services.AddMappings();

        return services;
    }
}