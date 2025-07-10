using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Configurations;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
    public MovieApiContext (DbContextOptions<MovieApiContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; } = default!;
    public DbSet<Actor> Actors { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<MovieDetails> MovieDetails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreConfigurations());
        modelBuilder.ApplyConfiguration(new MovieConfigurations());
        modelBuilder.ApplyConfiguration(new ActorConfigurations());
        modelBuilder.ApplyConfiguration(new ReviewConfigurations());
        modelBuilder.ApplyConfiguration(new MovieDetailsConfigurations());

    }
}
