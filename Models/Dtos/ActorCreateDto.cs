using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public record ActorCreateDto([Required] string Name, int BirthYear);
