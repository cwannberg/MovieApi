﻿namespace MovieApi.Models.Dtos;

public class MovieDetailDto
{
    public string Title { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int Year { get; set; }
    public string Synopsis { get; set; } = null!;
    public string Language { get; set; } = null!;
    public double Budget { get; set; }


    public IEnumerable<MovieActorDto> MovieActors { get; set; } = new List<MovieActorDto>();
    public IEnumerable<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
}
