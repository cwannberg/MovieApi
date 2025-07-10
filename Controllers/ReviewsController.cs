using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Get all reviews", Description = "Gets all reviews with their author and ratings.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview()
    {
        var reviewDto = await _context.Reviews
                .Select(r => new ReviewDto
                {
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating
                }).ToListAsync();

        if (!reviewDto.Any())
        {
            return Problem(
            detail: "No reviews were found in the database.",
            title: "No reviews",
            statusCode: 404,
            instance: HttpContext.Request.Path);
        }
        return Ok(reviewDto);
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get one review", Description = "Gets a specific review by its ID.")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Review))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var reviewDto = await _context.Reviews.FindAsync(id);

        if (reviewDto == null)
        {
            return Problem(
                detail: $"The review with ID {id} could not be found.",
                title: "Review Not Found",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }
        return Ok(reviewDto);
    }

    // PUT: api/Reviews/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a review", Description = "Updates a review by its ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutReview(int id, Review review)
    {
        if (id != review.Id)
        {
            return Problem(
                detail: "The provided ID does not match the review ID.",
                title: "ID Mismatch",
                statusCode: 400,
                instance: HttpContext.Request.Path
            );
        }

        if (!ReviewExists(id))
        {
            return Problem(
                detail: $"No review found with ID {id}.",
                title: "Review Not Found",
                statusCode: 404,
                instance: HttpContext.Request.Path
            );
        }

        _context.Entry(review).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return Ok();
    }

    // POST: api/Reviews
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new review", Description = "Creates a new review entry.")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Review))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        _context.Reviews.Add(review);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error data: " + ex.Message);
            throw;
        }

        return CreatedAtAction("GetReview", new { id = review.Id }, review);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a review", Description = "Deletes a review by its ID.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return Problem(
                    detail: $"Review with ID {id} was not found.",
                    title: "Review Not Found",
                    statusCode: 404,
                    instance: HttpContext.Request.Path
                );
        }
        _context.Reviews.Remove(review);
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

    private bool ReviewExists(int id)
    {
        return _context.Reviews.Any(e => e.Id == id);
    }
}
