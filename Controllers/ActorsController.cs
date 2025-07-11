using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieApi.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly MovieApiContext _context;

    public ActorsController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Actors
    [HttpGet]
    [SwaggerOperation(Summary = "Get all actors", Description = "Returns a list of all actors including the movies they appear in.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor()
    {
        var actorDto = await _context.Actors
            .Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear,
                Movies = a.Movies.Select(m => new ActorsMoviesDto()
                {
                    Title = m.Title
                }).ToList()
            }).ToListAsync();
        if (actorDto == null)
        {
            return Problem(
                detail: "No actors could be found in the database",
                title: "Actor missing",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }
        return Ok(actorDto);
    }

    // GET: api/Actors/5
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get actor by ID", Description = "Returns a single actor by ID including their movies.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        var actor = await _context.Actors
            .Where(a => a.Id == id)
            .Select(a => new ActorDto
            {
                Name = a.Name,
                BirthYear = a.BirthYear,
                Movies = a.Movies.Select(m => new ActorsMoviesDto()
                {
                    Title = m.Title
                }).ToList()
            }).FirstOrDefaultAsync(); ;

        if (actor == null)
        {
            return Problem(
                detail: $"Actor with ID {id} could not be found.",
                title: "Actor missing",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }
        return Ok(actor);
    }

    // PUT: api/Actors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update an actor", Description = "Updates an existing actor's name and birth year.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Actor>> PutActor(int id, ActorUpdateDto dto)
    {
        var actor = await _context.Actors
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actor == null)
        {
            return Problem(
                detail: $"Actor with ID {id} could not be found in the database.",
                title: "Actor missing",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }
        actor.Name = dto.Name;
        actor.BirthYear = dto.BirthYear;

        _context.Entry(actor).State = EntityState.Modified;
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

    // POST: api/Actors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [SwaggerOperation(Summary = "Create an actor", Description = "Adds a new actor to the database.")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDto))]
    public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto dto)
    {
        var existingMovie = await _context.Actors
     .FirstOrDefaultAsync(a => a.Name == dto.Name && a.BirthYear == dto.BirthYear);

        if (existingMovie != null)
        {
            return Conflict("This actor is already in the database");
        }
        var actor = new Actor
        {
            Name = dto.Name,
            BirthYear = dto.BirthYear
        };
        _context.Actors.Add(actor);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return CreatedAtAction(nameof(GetActor), new {id  = actor.Id}, actor);
    }

    // DELETE: api/Actors/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete actor", Description = "Deletes an actor by ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        { 
            return Problem(
                detail: $"Actor with ID {id} could not be found in the database.",
                title: "Actor missing",
                statusCode: 404,
                instance: HttpContext.Request.Path);
        }
        _context.Actors.Remove(actor);
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
