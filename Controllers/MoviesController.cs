using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Get all movies", Description = "Gets all movies with details, actors, and reviews.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDetailDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
    {
        var movieDto = await _context.Movies
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

            if (!movieDto.Any())
            {
                return Problem(
                     detail: "No movies could not be found in database.",
                     title: "Movie missing",
                     statusCode: 404,
                     instance: HttpContext.Request.Path);
            }
            return Ok(movieDto);
    }
    // GET: api/Movies/5
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get movie by ID", Description = "Gets a basic movie view by ID.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movieDto = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDto()
            {
                Title = m.Title,
                Genre = m.Genre.Name,
                Year = m.Year,
            })                  
            .FirstOrDefaultAsync();

        if (movieDto == null)
        {
            return Problem(
                 detail: "The movie could not be found in database.",
                 title: "Movie missing",
                 statusCode: 404,
                 instance: HttpContext.Request.Path);
        }
        return Ok(movieDto);
    }
    // GET: api/Movies/5
    [HttpGet("{id}/details")]
    [SwaggerOperation(Summary = "Get full movie details", Description = "Gets movie with full details including actors and reviews.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovieDto>> GetMovieDetails(int id)
    {
        var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieDetails)
                .Include(m => m.Actors)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null)
                {
                    return Problem(
                         detail: $"Movie with ID {id} not found.",
                         title: "Movie missing",
                         statusCode: 404,
                         instance: HttpContext.Request.Path);
                }

        var movieDto = new MovieDetailDto
        {
            Title = movie.Title,
            Genre = movie.Genre.Name,
            Year = movie.Year,
            Synopsis = movie.MovieDetails.Synopsis,
            Language = movie.MovieDetails.Language,
            Budget = movie.MovieDetails.Budget,
            Duration = movie.MovieDetails.Duration,

            Actors = movie.Actors.Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear
            }).ToList(),
            Reviews = movie.Reviews.Select(r => new ReviewDto
            {
                ReviewerName = r.ReviewerName,
                Rating = r.Rating
            }).ToList()
        };

        if (movieDto == null)
        {
            return Problem(
                 detail: $"Movie with ID {id} not found.",
                 title: "Movie missing",
                 statusCode: 404,
                 instance: HttpContext.Request.Path);
        }
        return Ok(movieDto);
    }
    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a movie", Description = "Updates a movie and its details.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
    {
        var movie = await _context.Movies
            .Include(m => m.MovieDetails)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return Problem(
                detail: $"Movie with ID {id} not found.",
                title: "Movie missing",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }
        if (movie.MovieDetails == null)
        {
            movie.MovieDetails = new MovieDetails();
        }
        movie.Title = dto.Title;
        movie.Year = dto.Year;
        movie.MovieDetails.Language = dto.Language;
        movie.MovieDetails.Budget = dto.Budget;
        movie.MovieDetails.Synopsis = dto.Synopsis;
        movie.MovieDetails.Duration = dto.Duration;
        movie.Genre.Name = dto.Genre;


        _context.Entry(movie).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [SwaggerOperation(Summary = "Create a movie", Description = "Creates a new movie with details and one actor.")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Movie))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto dto)
    {
        var existingMovie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Title == dto.Title && m.Year == dto.Year);

        if (existingMovie != null)
        {
            return Conflict("A movie with that name and year is already in the database.");
        }

        var movie = new Movie
        {
            Title = dto.Title,
            Year = dto.Year,
            GenreId = dto.GenreId,
            Actors = dto.ActorDto is not null
        ? new List<Actor>
        {
            new Actor
            {
                Name = dto.ActorDto.Name,
                BirthYear = dto.ActorDto.BirthYear
            }
        }
        : new List<Actor>(),
            MovieDetails = new MovieDetails
            {
                Duration = dto.Duration,
                Synopsis = dto.Synopsis,
                Language = dto.Language,
                Budget = dto.Budget
            }
        };
        _context.Entry(movie).State = EntityState.Modified;
        _context.Movies.Add(movie);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }
    //POST: api/movies/2/actors/1
    [HttpPost("{movieId}/actors/{actorId}")]
    public async Task<ActionResult<Movie>> AddActorToMovie(int movieId, int actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return Problem(
                 detail: $"Movie with ID {movieId} not found.",
                 title: "Movie missing",
                 statusCode: 404,
                 instance: HttpContext.Request.Path);
        }
        var actor = await _context.Actors
            .FirstOrDefaultAsync(a => a.Id == actorId);
        if (actor == null)
        {
            return Problem(
                 detail: $"Actor with ID {actorId} not found.",
                 title: "Actor missing",
                 statusCode: 404,
                 instance: HttpContext.Request.Path
            );
        }
        _context.Entry(movie).State = EntityState.Modified;
        movie.Actors.Add(actor);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        var movieDto = new MovieCreateDto
        {
            Id = movie.Id,
            Title = movie.Title
        };
        return Ok(movieDto);
    }
    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete movie", Description = "Deletes a movie by ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return Problem(
                detail: $"Movie with ID {id} not found.",
                title: "Movie missing",
                statusCode: 404,
                instance: HttpContext.Request.Path);
        }
        _context.Movies.Remove(movie);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return NoContent();
    }
}
