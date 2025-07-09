using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieConfigurations : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.Genre)
               .WithMany()
               .HasForeignKey(m => m.GenreId);

        builder.HasOne(m => m.MovieDetails);

        builder.HasMany(m => m.Reviews);

        builder.HasMany(m => m.Actors);
    }
}