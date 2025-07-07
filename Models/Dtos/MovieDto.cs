namespace MovieApi.Models.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public List<ReviewDto> Reviews { get; set; } = null!;
}
