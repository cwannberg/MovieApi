namespace MovieApi.Models.Entities;

public class MovieDetails
{
    public int Id { get; set; }
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int Budget { get; set; }
    public string Duration { get; set; } = null!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}
