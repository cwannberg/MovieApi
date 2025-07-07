using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
    public MovieApiContext (DbContextOptions<MovieApiContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureGenre(modelBuilder);
        ConfigureMovie(modelBuilder);
        ConfigureMovieActor(modelBuilder);
        ConfigureMovieDetails(modelBuilder);
        ConfigureReview(modelBuilder);
        ConfigureMovieActorRelation(modelBuilder);
    }
    private void ConfigureMovie(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
        .HasData(
                new Movie { Id = 1, Title = "Amelie från Montemartre", GenreId = 1, Duration = "2h, 20 min", Year = 2001 },
                new Movie { Id = 2, Title = "Aladdin", GenreId = 3, Duration = "1h, 15 min", Year = 1992, },
                new Movie { Id = 3, Title = "Jurassic Park", GenreId = 2, Duration = "2h, 45 min", Year = 1993 },
                new Movie { Id = 4, Title = "Deadpool and Wolverine",GenreId = 2, Duration = "3h, 10 min", Year = 2024 }
                ); ;
    }
    private void ConfigureReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, ReviewerName = "Anna", Rating = 5, MovieId = 1 },
                new Review { Id = 2, ReviewerName = "Johan", Rating = 4, MovieId = 2 },
                new Review { Id = 3, ReviewerName = "Lisa", Rating = 3, MovieId = 3 },
                new Review { Id = 4, ReviewerName = "Erik", Rating = 4, MovieId = 4 },
                new Review { Id = 5, ReviewerName = "Sofia", Rating = 5, MovieId = 1 }
        );
    }

    private void ConfigureMovieActorRelation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasMany(m => m.MovieActors)
            .WithMany(ma => ma.Movies)
            .UsingEntity(j => j.HasData(
                new { MovieActorsId = 1, MoviesId = 1 },
                new { MovieActorsId = 2, MoviesId = 1 },
                new { MovieActorsId = 3, MoviesId = 2 },
                new { MovieActorsId = 4, MoviesId = 3 },
                new { MovieActorsId = 5, MoviesId = 4 }
            ));
    }
    private void ConfigureMovieDetails(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieDetails>(entity =>
        {
            entity
                .HasOne(m => m.Movie)
                .WithOne(m => m.MovieDetails)
                .HasForeignKey<MovieDetails>(m => m.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieDetails>().HasData(
            new MovieDetails
            {
                Id = 1,
                MovieId = 1,
                Synopsis = "En charmig och poetisk film om Amelie i Montmartre.",
                Language = "Franska",
                Budget = 10000000
            },
            new MovieDetails
            {
                Id = 2,
                MovieId = 2,
                Synopsis = "Ett klassiskt äventyr med en magisk lampa och en ande.",
                Language = "Engelska",
                Budget = 15000000
            },
            new MovieDetails
            {
                Id = 3,
                MovieId = 3,
                Synopsis = "En spännande thriller med levande dinosaurier i en nöjespark.",
                Language = "Engelska",
                Budget = 60000000
            },
            new MovieDetails
            {
                Id = 4,
                MovieId = 4,
                Synopsis = "Humoristisk superhjältefilm med Deadpool och Wolverine.",
                Language = "Engelska",
                Budget = 80000000
            });
        });
    }

    private void ConfigureMovieActor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { Id = 1, Name = "Brad Pitt", BirthYear = 1971 },
                new MovieActor { Id = 2, Name = "Meryl Streep", BirthYear = 1949 },
                new MovieActor { Id = 3, Name = "Leonardo DiCaprio", BirthYear = 1974 },
                new MovieActor { Id = 4, Name = "Emma Stone", BirthYear = 1988 },
                new MovieActor { Id = 5, Name = "Tom Hanks", BirthYear = 1956 }
                );
    }

    private void ConfigureGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Drama" },
                new Genre { Id = 2, Name = "Action" },
                new Genre { Id = 3, Name = "Children" }
                );  
    }
}
