using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public record MovieCreateDto {
    public int Id { get; set; }
    [Required]
    public string Title { get; init; } = null!;
    [Required]
    public int Year { get; init; }
    [Range(1, 4)]
    public int GenreId { get; init; }
    public string Synopsis { get; init; } = null!;
    public string Language { get; init; } = null!;
    public int Budget { get; init; }
    public string Duration { get; init; } = null!;
    public string Genre {  get; init; } = null!;
    public IEnumerable<MovieDto>? Movies { get; init; } = new List<MovieDto>();

    public ActorDto? ActorDto { get; init; }
    public IEnumerable<ActorDto>? Actors { get; init; } = new List<ActorDto>();
};
