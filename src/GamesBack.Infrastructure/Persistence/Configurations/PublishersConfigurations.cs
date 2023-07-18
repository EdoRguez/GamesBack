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
}