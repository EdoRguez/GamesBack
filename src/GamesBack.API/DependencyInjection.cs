using System.Text.Json.Serialization;
using Carter;
using GamesBack.API.Common.Mapping;
using GamesBack.API.Middlewares;
using Microsoft.AspNetCore.Http.Json;

namespace GamesBack.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            string? connection = configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        services.AddCarter();

        services.AddMappings();

        return services;
    }
}