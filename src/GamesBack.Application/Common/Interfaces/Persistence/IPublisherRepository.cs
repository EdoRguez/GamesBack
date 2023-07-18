namespace GamesBack.Application.Common.Interfaces.Persistence;

public interface IPublisherRepository
{
    bool Exists(Guid id);
}