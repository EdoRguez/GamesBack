using GamesBack.Contracts.Publisher;
using GamesBack.Domain.PublisherAggregate;
using Mapster;

namespace GamesBack.API.Common.Mapping;

public class PublisherMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Publisher, PublisherResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}