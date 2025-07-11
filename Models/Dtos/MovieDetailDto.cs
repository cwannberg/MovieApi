using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public class MovieDetailDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public int Year { get; set; }
    public string? Genre { get; set; } 
    public string? Synopsis { get; set; } 
    public string? Language { get; set; } 
    public int? Budget { get; set; } 
    public string? Duration { get; set; } 


    public IEnumerable<ActorDto>? Actors { get; set; } = new List<ActorDto>();
    public IEnumerable<ReviewDto>? Reviews { get; set; } = new List<ReviewDto>();
}
