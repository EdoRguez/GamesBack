using GamesBack.Domain.GameAggregate;
using GamesBack.Domain.GameAggregate.Entities;
using GamesBack.Domain.GameAggregate.ValueObjects;
using GamesBack.Domain.PublisherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesBack.Infrastructure.Persistence.Configurations;

public class GamesConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        ConfigureGames(builder);
        ConfigureGenresTable(builder);
    }

    private void ConfigureGames(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GameId.Create(value)
            )
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PublisherId)
            .HasConversion(
                id => id.Value,
                value => PublisherId.Create(value)
            )
            .IsRequired();

        builder.HasOne(x => x.Publisher)
            .WithMany(x => x.Games);
    }

    private void ConfigureGenresTable(EntityTypeBuilder<Game> builder)
    {
        builder.OwnsMany(x => x.Reviews, x =>
        {
            x.ToTable("Reviews");

            x.WithOwner().HasForeignKey("GameId");

            x.HasKey(nameof(Review.Id), "GameId");

            x.Property(x => x.Id)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReviewId.Create(value)
                );

            x.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();

            x.Property(x => x.Rating)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(Game.Reviews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}