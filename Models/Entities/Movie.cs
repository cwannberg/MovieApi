namespace MovieApi.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = String.Empty;
        public double Duration { get; set; }



        //1-1
        public MovieDetails MovieDetails { get; set; } = null!;

        //1-M
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        //N-M
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
