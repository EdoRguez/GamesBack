using GamesBack.Domain.GameAggregate;

namespace GamesBack.Application.Common.Interfaces.Persistence;

public interface IGameRepository
{
    Task<Game?> Get(Guid id, bool trackChanges = false, string[]? includes = null);
    Task<IEnumerable<Game>> GetAll(bool trackChanges = false, string[]? includes = null);
    void Add(Game game);
    void Delete(Game game);
    Task<bool> Exists(Guid id);
}