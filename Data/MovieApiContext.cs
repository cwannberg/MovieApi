using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MovieApiContext : DbContext
{
    public MovieApiContext (DbContextOptions<MovieApiContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}
