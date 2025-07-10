﻿using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class SeedData
{
    internal static async Task InitAsync(Func<MovieApiContext> getContext)
    {
        using var context = getContext();
        //await GenreSeeds(context);
        await MovieSeeds(context);
        await ActorSeeds(context);
        await MovieDetailsSeeds(context);
        await ReviewSeeds(context);
    }
    //public static async Task GenreSeeds(MovieApiContext db)
    //{
    //    if (await db.Genres.AnyAsync()) return;

    //    var genres = new List<Genre>
    //    {
    //            new() { Name = "Drama" },
    //            new() { Name = "Action" },
    //            new() { Name = "Children" }
    //    };
    //    db.Genres.AddRange(genres);
    //    await db.SaveChangesAsync();
    //}

    public static async Task ActorSeeds(MovieApiContext db)
    {
        if (await db.Actors.AnyAsync()) return;

        var actors = new List<Actor>
        {
            new() { Name = "Brad Pitt", BirthYear = 1971 },
            new() { Name = "Meryl Streep", BirthYear = 1949 },
            new() { Name = "Leonardo DiCaprio", BirthYear = 1974 },
            new() { Name = "Emma Stone", BirthYear = 1988 },
            new() { Name = "Tom Hanks", BirthYear = 1956 }
        };
        db.Actors.AddRange(actors);
        await db.SaveChangesAsync();
    }
    public static async Task MovieSeeds(MovieApiContext db)
    {
        if (await db.Movies.AnyAsync()) return;
        var movies = new List<Movie>
        {
            new() { Title = "Amelie från Montemartre", GenreId = 1, Duration = "2h, 20 min", Year = 2001 },
            new() { Title = "Aladdin", GenreId = 1, Duration = "1h, 15 min", Year = 1992, },
            new() { Title = "Jurassic Park", GenreId = 1, Duration = "2h, 45 min", Year = 1993 },
            new() { Title = "Deadpool and Wolverine", GenreId = 2, Duration = "3h, 10 min", Year = 2024 }
        };
        db.Movies.AddRange(movies);
        await db.SaveChangesAsync();
    }
    public static async Task MovieDetailsSeeds(MovieApiContext db)
    {
        if (await db.MovieDetails.AnyAsync()) return;
        var movieDetails = new List<MovieDetails>
        {
            new MovieDetails
            {
                MovieId = 1,
                Synopsis = "En charmig och poetisk film om Amelie i Montmartre.",
                Language = "Franska",
                Budget = 10000000,
                Duration = "2h, 5min"
            },
            new MovieDetails
            {
                MovieId = 2,
                Synopsis = "Ett klassiskt äventyr med en magisk lampa och en ande.",
                Language = "Engelska",
                Budget = 15000000,
                Duration = "1h, 52min"
            },
            new MovieDetails
            {
                MovieId = 3,
                Synopsis = "En spännande thriller med levande dinosaurier i en nöjespark.",
                Language = "Engelska",
                Budget = 60000000,
                Duration = "1h, 35min"
            },
            new MovieDetails
            {
                MovieId = 4,
                Synopsis = "Humoristisk superhjältefilm med Deadpool och Wolverine.",
                Language = "Engelska",
                Budget = 80000000,
                Duration = "2h, 34min"
            }
        };
        db.MovieDetails.AddRange(movieDetails);
        await db.SaveChangesAsync();
    }
    public static async Task ReviewSeeds(MovieApiContext db)
    {
        if (await db.Reviews.AnyAsync()) return;
        var reviews = new List<Review>
        {
            new() { ReviewerName = "Johan", Rating = 4, MovieId = 2 },
            new() { ReviewerName = "Lisa", Rating = 3, MovieId = 3 },
            new() { ReviewerName = "Erik", Rating = 4, MovieId = 4 },
            new() { ReviewerName = "Sofia", Rating = 5, MovieId = 1 }
        };
        db.Reviews.AddRange(reviews);
        await db.SaveChangesAsync();
    }
}