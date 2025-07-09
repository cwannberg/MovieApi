using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers;

[Route("api/movies")]
[ApiController]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly MovieApiContext _context;

    public MoviesController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
    {
        var dto = await _context.Movies
                .Select(m => new MovieDetailDto()
                {
                    Title = m.Title,
                    Genre = m.Genre.Name,
                    Year = m.Year,

                    Synopsis = m.MovieDetails.Synopsis,
                    Language = m.MovieDetails.Language,
                    Budget = m.MovieDetails.Budget,
                    Duration = m.MovieDetails.Duration,

                    Actors = m.Actors.Select(a => new ActorDto
                    {
                        Name = a.Name,
                        BirthYear = a.BirthYear,
                        Movies = a.Movies.Select(m => new ActorsMoviesDto
                        {
                            Title = m.Title
                        }).ToList(),
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewDto
                    {
                        ReviewerName = r.ReviewerName,
                        Rating = r.Rating
                    }).ToList()                   
                }).ToListAsync();
        return Ok(dto);
    }
    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDto()
            {
                Title = m.Title,
                Genre = m.Genre.Name,
                Year = m.Year,
            })                  
            .FirstOrDefaultAsync();

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }
    // GET: api/Movies/5
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDto>> GetMovieDetails(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDetailDto()
            {
                Title = m.Title,
                Genre = m.Genre.Name,
                Year = m.Year,
                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget,
                Duration = m.MovieDetails.Duration,

                Actors = m.Actors.Select(a => new ActorDto
                {
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList(),
                Reviews = m.Reviews.Select(r => new ReviewDto
                    {
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }
    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieDetails)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        movie.Title = dto.Title;
        movie.Year = dto.Year;
        movie.MovieDetails.Language = dto.Language;
        movie.MovieDetails.Budget = dto.Budget;
        movie.MovieDetails.Synopsis = dto.Synopsis;
        movie.MovieDetails.Duration = dto.Duration;
        movie.Genre.Name = dto.Genre;

        _context.Entry(movie).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            Year = dto.Year,
            GenreId = dto.GenreId,
            Actors = new List<Actor>
            {
                new Actor
                {
                    Name = dto.ActorDto.Name,
                    BirthYear = dto.ActorDto.BirthYear
                }
            },
            MovieDetails = new MovieDetails
            {
                Duration = dto.Duration,
                Synopsis = dto.Synopsis,
                Language = dto.Language,
                Budget = dto.Budget
            }
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }
    //POST: api/movies/2/actors/1
    [HttpPost("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<Movie>> AddActorToMovie(int movieId, int actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        var actor = await _context.Actors
            .FirstOrDefaultAsync(a => a.Id == actorId);      


        movie.Actors.Add(actor);
        await _context.SaveChangesAsync();

        var movieDto = new MovieCreateDto
        {
            Id = movie.Id,
            Title = movie.Title
        };
        return Ok(movieDto);
    }
    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
