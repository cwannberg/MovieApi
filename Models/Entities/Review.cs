using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string ReviewerName { get; set; } = null!;
        [Range(1, 5)]
        [Required]
        public int Rating { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
