namespace MovieApi.Models.Dtos;

public record MovieUpdateDto(string Title, string Genre, int Year, string Synopsis, string Language, int Budget);