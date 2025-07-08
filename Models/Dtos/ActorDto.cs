namespace MovieApi.Models.Dtos;

public class ActorDto
{
    public ActorDto(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }

    public string Name { get; set; }
    public int BirthYear { get; set; }

    public ICollection<ActorsMoviesDto> Movies { get; set; } = new List<ActorsMoviesDto>();
}
