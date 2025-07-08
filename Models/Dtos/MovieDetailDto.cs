using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public class MovieDetailDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public int Year { get; set; }
    public string Genre { get; set; } = null!;
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public int? Budget { get; set; } 
    public string Duration { get; set; } = null!;


    public IEnumerable<ActorDto>? Actors { get; set; } = new List<ActorDto>();
    public IEnumerable<ReviewDto>? Reviews { get; set; } = new List<ReviewDto>();
}
