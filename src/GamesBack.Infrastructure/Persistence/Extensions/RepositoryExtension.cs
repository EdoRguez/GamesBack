using Microsoft.EntityFrameworkCore;

namespace GamesBack.Infrastructure.Persistence.Extensions;

public static class RepositoryExtension
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[]? includes) where T : class
    {
        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}