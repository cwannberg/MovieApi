using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations
{
    public class ActorConfigurations : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasData(
                new Actor { Id = 1, Name = "Brad Pitt", BirthYear = 1971 },
                new Actor { Id = 2, Name = "Meryl Streep", BirthYear = 1949 },
                new Actor { Id = 3, Name = "Leonardo DiCaprio", BirthYear = 1974 },
                new Actor { Id = 4, Name = "Emma Stone", BirthYear = 1988 },
                new Actor { Id = 5, Name = "Tom Hanks", BirthYear = 1956 }
                );
        }
    }
}
