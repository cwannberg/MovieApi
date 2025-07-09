using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;
using System.Reflection.Emit;

namespace MovieApi.Data.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
            builder.HasData(
        new Review { Id = 1, ReviewerName = "Anna", Rating = 5, MovieId = 1 },
        new Review { Id = 2, ReviewerName = "Johan", Rating = 4, MovieId = 2 },
        new Review { Id = 3, ReviewerName = "Lisa", Rating = 3, MovieId = 3 },
        new Review { Id = 4, ReviewerName = "Erik", Rating = 4, MovieId = 4 },
        new Review { Id = 5, ReviewerName = "Sofia", Rating = 5, MovieId = 1 }
);
    }
}
