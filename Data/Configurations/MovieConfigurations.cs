using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MovieConfigurations : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasData(
            new Movie { Id = 1, Title = "Amelie från Montemartre", GenreId = 1, Duration = "2h, 20 min", Year = 2001 },
            new Movie { Id = 2, Title = "Aladdin", GenreId = 3, Duration = "1h, 15 min", Year = 1992, },
            new Movie { Id = 3, Title = "Jurassic Park", GenreId = 2, Duration = "2h, 45 min", Year = 1993 },
            new Movie { Id = 4, Title = "Deadpool and Wolverine", GenreId = 2, Duration = "3h, 10 min", Year = 2024 }
            ); ;
    }
}
