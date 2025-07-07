namespace MovieApi.Models.Entities;

public interface IActor
{
    int Id { get; set; }
    string Name { get; set; }
    int BirthYear { get; set; }
}
