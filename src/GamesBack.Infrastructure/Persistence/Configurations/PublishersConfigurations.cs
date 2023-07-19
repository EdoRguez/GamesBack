using GamesBack.Domain.PublisherAggregate;
using GamesBack.Domain.PublisherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesBack.Infrastructure.Persistence.Configurations;

public class PublishersConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        ConfigurePublishers(builder);
        ConfigureData(builder);
    }

    private void ConfigurePublishers(EntityTypeBuilder<Publisher> builder)
    {
        builder.ToTable("Publishers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PublisherId.Create(value)
            )
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(x => x.Games)
            .WithOne(x => x.Publisher);
    }

    private void ConfigureData(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasData(
            Publisher.Create(Guid.Parse("10bdf658-4d44-4b08-9e9d-6e428c0d7599"), "Ubisoft")
        );
    }
}