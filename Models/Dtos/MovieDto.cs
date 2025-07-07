using MovieApi.Models.Entities;

namespace MovieApi.Models.Dtos;

public class MovieDto
{
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Duration { get; set; } = null!;
    public string Genre { get; set; } = null!;
}
