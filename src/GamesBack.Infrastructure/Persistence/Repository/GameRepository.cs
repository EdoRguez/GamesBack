using GamesBack.Application.Common.Interfaces.Persistence;
using GamesBack.Domain.GameAggregate;
using GamesBack.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GamesBack.Infrastructure.Persistence.Repository;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _appDbContext;

    public GameRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Game?> Get(Guid id, bool trackChanges = false, string[]? includes = null)
    {
        if(!trackChanges)
        {
            return await _appDbContext.Games
                                        .AsNoTracking()
                                        .IncludeMultiple(includes)
                                        .SingleOrDefaultAsync(x => x.Id.Value == id);
        }

        return await _appDbContext.Games
                                        .IncludeMultiple(includes)
                                        .SingleOrDefaultAsync(x => x.Id.Value == id);
    }

    public async Task<IEnumerable<Game>> GetAll(bool trackChanges = false, string[]? includes = null)
    {
        if(!trackChanges)
        {
            return await _appDbContext.Games
                                        .AsNoTracking()
                                        .IncludeMultiple(includes)
                                        .ToListAsync();
        }

        return await _appDbContext.Games
                                        .IncludeMultiple(includes)
                                        .ToListAsync();
    }

    public void Add(Game game)
    {
        _appDbContext.Add(game);
    }
}