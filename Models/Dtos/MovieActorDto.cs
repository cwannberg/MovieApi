namespace MovieApi.Models.Dtos;

public class MovieActorDto
{
    public MovieActorDto(int birthYear)
    {
        BirthYear = birthYear;
    }

    public string Name { get; set; }
    public int BirthYear { get; set; }
}
