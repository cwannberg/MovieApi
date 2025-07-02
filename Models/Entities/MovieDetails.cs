namespace MovieApi.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = String.Empty;
        public string Languague { get; set; } = String.Empty;
        public double Budget { get; set; }

        //1-1
        public Movie Movie { get; set; }

        public int MovieId { get; set; }
    }
}
