using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

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
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor()
    {
        var dto = await _context.Actors
                .Select(a => new ActorDto
                {
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                    Movies = a.Movies.Select(m => new ActorsMoviesDto()
                    {
                        Title = m.Title
                    }).ToList()
                }).ToListAsync();

        return Ok(dto);
    }

    // GET: api/Actors/5
    [HttpGet("{id}")]
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

        if (actor == null) return Problem(
                        detail: "The actor could not be found.",
                        title: "Actor missing",
                        statusCode: 404);

        return Ok(actor);
    }

    // PUT: api/Actors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<Actor>> PutActor(int id, ActorUpdateDto dto)
    {
        var actor = await _context.Actors
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actor == null) return Problem(
                 detail: "The actor could not be found in the database.",
                 title: "Actor missing",
                 statusCode: 404);

        actor.Name = dto.Name;
        actor.BirthYear = dto.BirthYear;

        _context.Entry(actor).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent(); 
    }

    // POST: api/Actors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto dto)
    {
        var actor = new Actor
        {
            Name = dto.Name,
            BirthYear = dto.BirthYear
        };
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActor), new {id  = actor.Id}, actor);
    }

    // DELETE: api/Actors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null) return Problem(
                 detail: "The actor could not be found in the database.",
                 title: "Actor missing",
                 statusCode: 404);

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActorExists(int id)
    {
        return _context.Actors.Any(e => e.Id == id);
    }
}
