using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public class ActorUpdateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
    }
}
