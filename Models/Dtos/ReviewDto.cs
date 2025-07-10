using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public class ReviewDto
{
    public string ReviewerName { get; set; } = null!;
    [Range(1, 5)]
    [Required]
    public int Rating { get; set; }
}