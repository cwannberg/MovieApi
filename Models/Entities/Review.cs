using System.Text.Json.Serialization;

namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public int Rating { get; set; }

        //M-1
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
