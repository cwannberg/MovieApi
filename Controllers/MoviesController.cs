using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers;

[Route("api/Movies")]
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

                    MovieActors = m.MovieActors.Select(a => new MovieActorDto(a.BirthYear)
                    {
                        Name = a.Name
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewDto(r.Rating)
                    {
                        ReviewerName = r.ReviewerName
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
            .Select(m => new MovieDetailDto() 
            {

                Title = m.Title,
                Genre = m.Genre.Name,
                Year = m.Year,

                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget,
                    MovieActors = m.MovieActors.Select(a => new MovieActorDto(a.BirthYear)
                    {
                        Name = a.Name
                    }).ToList(),
                    Reviews = m.Reviews.Select(r => new ReviewDto(r.Rating)
                    {
                        ReviewerName = r.ReviewerName
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
            .Include(m => m.MovieActors)
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        movie.Title = dto.Title;
        movie.Year = dto.Year;
        movie.MovieDetails.Language = dto.Language;
        movie.MovieDetails.Budget = dto.Budget;
        movie.MovieDetails.Synopsis = dto.Synopsis;
        movie.Genre.Name = dto.Genre;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostMovie(MovieCreateDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            Year = dto.Year,
            Duration = dto.Duration,
            GenreId = dto.GenreId,
            MovieDetails = new MovieDetails
            {
                Synopsis = dto.Synopsis,
                Language = dto.Language,
                Budget = dto.Budget
            }
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
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
