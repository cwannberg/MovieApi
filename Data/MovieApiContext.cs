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

        ConfigureMovie(modelBuilder);
        ConfigureGenre(modelBuilder);
        ConfigureActor(modelBuilder);
        ConfigureActorMovie(modelBuilder);
        ConfigureMovieDetails(modelBuilder);
        ConfigureReview(modelBuilder);
    }

    private void ConfigureReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.ReviewerName)
                .IsRequired();

            entity.Property(r => r.Rating)
                .IsRequired();

            entity.HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>().HasData(
                    new Review { Id = 1, ReviewerName = "Anna", Rating = 5, MovieId = 1 },
                    new Review { Id = 2, ReviewerName = "Johan", Rating = 4, MovieId = 1 },
                    new Review { Id = 3, ReviewerName = "Lisa", Rating = 3, MovieId = 2 },
                    new Review { Id = 4, ReviewerName = "Erik", Rating = 4, MovieId = 3 },
                    new Review { Id = 5, ReviewerName = "Sofia", Rating = 5, MovieId = 4 }
        );
    }

    private void ConfigureMovieDetails(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieDetails>(entity =>
        {
            entity.HasKey(md => md.MovieId);
            entity.Property(md => md.Synopsis).IsRequired();
            entity.Property(md => md.Languague).IsRequired();
            entity.Property(md => md.Budget);

            entity.HasOne(md => md.Movie)
                  .WithOne(md => md.MovieDetails)
                  .HasForeignKey<MovieDetails>(md => md.MovieId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<MovieDetails>().HasData(
            new MovieDetails
            {
                MovieId = 1,
                Synopsis = "En charmig och poetisk film om Amelie i Montmartre.",
                Languague = "Franska",
                Budget = 10000000
            },
            new MovieDetails
            {
                MovieId = 2,
                Synopsis = "Ett klassiskt äventyr med en magisk lampa och en ande.",
                Languague = "Engelska",
                Budget = 15000000
            },
            new MovieDetails
            {
                MovieId = 3,
                Synopsis = "En spännande thriller med levande dinosaurier i en nöjespark.",
                Languague = "Engelska",
                Budget = 60000000
            },
            new MovieDetails
            {
                MovieId = 4,
                Synopsis = "Humoristisk superhjältefilm med Deadpool och Wolverine.",
                Languague = "Engelska",
                Budget = 80000000
            }  
        );
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

    private void ConfigureActor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>().HasData(
                    new Actor { Id = 1, Name = "Brad Pitt", BirthYear = 1971},
                    new Actor { Id = 2, Name = "Meryl Streep", BirthYear = 1949 },
                    new Actor { Id = 3, Name = "Leonardo DiCaprio", BirthYear = 1974 },
                    new Actor { Id = 4, Name = "Emma Stone", BirthYear = 1988 },
                    new Actor { Id = 5, Name = "Tom Hanks", BirthYear = 1956 }
                    );
    }

    private void ConfigureGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
                    new Genre { Id = 1, Name = "Drama" },
                    new Genre { Id = 2, Name = "Action" },
                    new Genre { Id = 3, Name = "Children" });
    
    }

    private void ConfigureMovie(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasData(
                    new Movie { Id = 1, Title = "Amelie från Montemartre", GenreId = 1, Duration = 2.2, Year = 2001 },
                    new Movie { Id = 2, Title = "Aladdin", GenreId = 3, Duration = 1.2, Year = 1992 },
                    new Movie { Id = 3, Title = "Jurassic Park", GenreId = 2, Duration = 1.5, Year = 1993 },
                    new Movie { Id = 4, Title = "Deadpool and Wolverine", GenreId = 2, Duration = 2.4, Year = 2024 }
                    );      
    }
}
