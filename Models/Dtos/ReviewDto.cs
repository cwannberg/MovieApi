namespace MovieApi.Models.Dtos;

public class ReviewDto
{
    public ReviewDto(int rating)
    {
        Rating = rating;
    }

    public string ReviewerName { get; set; }
    public int Rating { get; set; }
}