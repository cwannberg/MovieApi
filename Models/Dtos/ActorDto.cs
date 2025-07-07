namespace MovieApi.Models.Dtos;

public class ActorDto
{
    public ActorDto(int birthYear)
    {
        BirthYear = birthYear;
    }

    public string Name { get; set; }
    public int BirthYear { get; set; }
}
