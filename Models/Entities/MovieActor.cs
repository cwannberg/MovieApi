﻿namespace MovieApi.Models.Entities;

public class MovieActor : IActor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BirthYear { get; set; }

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
