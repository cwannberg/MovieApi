using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieDetailsConfigurations : IEntityTypeConfiguration<MovieDetails>
{
    public void Configure(EntityTypeBuilder<MovieDetails> builder)
    {
        builder.HasKey(md => md.Id);
        builder.HasOne(md => md.Movie)
               .WithOne(md => md.MovieDetails)
               .HasForeignKey<MovieDetails>(md => md.MovieId); 
    }
}

