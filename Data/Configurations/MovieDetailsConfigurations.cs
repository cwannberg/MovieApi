using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieDetailsConfigurations : IEntityTypeConfiguration<MovieDetails>
{
    public void Configure(EntityTypeBuilder<MovieDetails> builder)
    {
        builder.HasOne(m => m.Movie)
                .WithOne(m => m.MovieDetails)
                .HasForeignKey<MovieDetails>(m => m.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new MovieDetails
            {
                Id = 1,
                MovieId = 1,
                Synopsis = "En charmig och poetisk film om Amelie i Montmartre.",
                Language = "Franska",
                Budget = 10000000,
                Duration = "2h, 5min"
            },
            new MovieDetails
            {
                Id = 2,
                MovieId = 2,
                Synopsis = "Ett klassiskt äventyr med en magisk lampa och en ande.",
                Language = "Engelska",
                Budget = 15000000,
                Duration = "1h, 52min"
            },
            new MovieDetails
            {
                Id = 3,
                MovieId = 3,
                Synopsis = "En spännande thriller med levande dinosaurier i en nöjespark.",
                Language = "Engelska",
                Budget = 60000000,
                Duration = "1h, 35min"
            },
            new MovieDetails
            {
                Id = 4,
                MovieId = 4,
                Synopsis = "Humoristisk superhjältefilm med Deadpool och Wolverine.",
                Language = "Engelska",
                Budget = 80000000,
                Duration = "2h, 34min"
            });
        });
    }
}
