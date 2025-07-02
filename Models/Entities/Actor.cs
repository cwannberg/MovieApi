namespace MovieApi.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int BirthYear { get; set; }

        // N-M
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
