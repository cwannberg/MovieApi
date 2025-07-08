using MovieApi.Models.Entities;

namespace MovieApi.Models.Dtos
{
    public class ActorCreateDto
    {
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
        public Movie Movie { get; set; }
    }
}
