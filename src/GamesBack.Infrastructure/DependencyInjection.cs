using GamesBack.Application.Common.Interfaces.Caching;
using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Infrastructure.Caching;
using GamesBack.Infrastructure.Persistence;
using GamesBack.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesBack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext()
            .AddPersistance()
            .AddCaching();

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=GamesDB;User Id=postgres;Password=admin;");
        });

        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}