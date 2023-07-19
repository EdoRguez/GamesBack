using GamesBack.Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamesBack.Infrastructure.Persistence.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly AppDbContext _db;

    public PublisherRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Exists(Guid id)
    {
        var publishers = await _db.Publishers.ToListAsync();
        return publishers.Any(x => x.Id.Value == id);
    }
}