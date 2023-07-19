namespace GamesBack.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    Task<bool> Exists(Guid id);
}