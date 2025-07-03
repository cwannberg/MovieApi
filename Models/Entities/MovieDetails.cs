using System.Text.Json.Serialization;

namespace MovieApi.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;
        public double Budget { get; set; }


        //FK
        public int MovieId { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; } = null!;
    }
}
