using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;
    public class GenreConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
                builder.HasData(
                new Genre { Id = 1, Name = "Drama" },
                new Genre { Id = 2, Name = "Action" },
                new Genre { Id = 3, Name = "Children" }
                );
    }
    }
}
