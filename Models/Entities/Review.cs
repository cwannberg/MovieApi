namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = String.Empty;
        public int Rating { get; set; }

        //M-1
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}
