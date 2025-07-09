using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models.Entities;
using System.Reflection.Emit;

namespace MovieApi.Data.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.Movie)
               .WithMany()
               .HasForeignKey(r => r.MovieId);
    }
}
