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
        ConfigureActorMovie(modelBuilder);
    }
    private void ConfigureActorMovie(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity("ActorMovie").HasData(
                    new { ActorsId = 1, MoviesId = 4 },
                    new { ActorsId = 2, MoviesId = 1 },
                    new { ActorsId = 3, MoviesId = 3 },
                    new { ActorsId = 4, MoviesId = 2 },
                    new { ActorsId = 5, MoviesId = 3 }
        );
    }
}
