using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public record ActorCreateDto(int Id, [Required] string Name, int BirthYear);
