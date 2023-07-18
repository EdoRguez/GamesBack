using GamesBack.Application.Common.Interfaces.Persistence;

namespace GamesBack.Infrastructure.Persistence.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly AppDbContext _db;

    public PublisherRepository(AppDbContext db)
    {
        _db = db;
    }

    public bool Exists(Guid id)
    {
        return _db.Publishers.Any(x => x.Id.Value == id);
    }
}