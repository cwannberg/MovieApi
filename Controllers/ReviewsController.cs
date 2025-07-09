using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly MovieApiContext _context;

    public ReviewsController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview()
    {
        var reviewDto = await _context.Reviews
                .Select(r => new ReviewDto
                {
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating
                }).ToListAsync();
        if (reviewDto == null) return Problem(
         detail: "No reviews could not be found in the database.",
         title: "Review missing",
         statusCode: 404);

        return Ok(reviewDto);
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var reviewDto = await _context.Reviews.FindAsync(id);

        if (reviewDto == null) return Problem(
                 detail: "The actor could not be found in the database.",
                 title: "Actor missing",
                 statusCode: 404);

        return Ok(reviewDto);
    }

    // PUT: api/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, Review review)
    {
        if (id != review.Id){ 
            return Problem(
                 detail: "The review could not be found in the database.",
                 title: "Review missing",
                 statusCode: 404);
        }

        _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        return Ok();
    }

    // POST: api/Reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReview", new { id = review.Id }, review);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ReviewExists(int id)
    {
        return _context.Reviews.Any(e => e.Id == id);
    }
}
