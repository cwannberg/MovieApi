using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos;

public record MovieCreateDto([Required] string Title, [Range(1, 4)] int GenreId, [Required] int Year, string Synopsis, string Language, int Budget, string Duration);
