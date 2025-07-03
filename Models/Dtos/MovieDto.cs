namespace MovieApi.Models.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<ReviewDto> Reviews { get; set; }
}

public class ReviewDto
{
    public int Id { get; set; }
    public string Text { get; set; }

}
