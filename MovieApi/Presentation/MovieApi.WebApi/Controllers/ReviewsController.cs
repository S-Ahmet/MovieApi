using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Domain.Entities;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Dto.Dtos.ReviewDtos;
using MovieApi.Persistence.Context;

namespace MovieApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewsController(MovieContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> PostReview(CreateReviewDto dto)
        {
            var review = new Review
            {
                MovieId = dto.MovieId,
                ReviewerName = dto.ReviewerName,
                ReviewComment = dto.ReviewComment,
                UserRating = dto.UserRating,
                ReviewDate = dto.ReviewDate,
                StarCount = dto.StarCount,
                Status = true
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewDto dto)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.ReviewID == id);
            if (review == null)
                return NotFound();

            review.ReviewComment = dto.ReviewComment;
            review.UserRating = dto.UserRating;
            review.ReviewDate = dto.ReviewDate;
            review.ReviewerName = dto.ReviewerName;
            review.StarCount = dto.StarCount;
            review.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // Get by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await _context.Reviews
                .Select(r => new ResultReviewDto
                {
                    ReviewID = r.ReviewID,
                    ReviewComment = r.ReviewComment,
                    UserRating = r.UserRating,
                    ReviewDate = r.ReviewDate,
                    ReviewerName = r.ReviewerName,
                    StarCount = r.StarCount,
                    MovieId = r.MovieId
                })
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // Get all by MovieId
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetReviewsByMovieId(int movieId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .Select(r => new ResultReviewDto
                {
                    ReviewID = r.ReviewID,
                    ReviewComment = r.ReviewComment,
                    UserRating = r.UserRating,
                    ReviewDate = r.ReviewDate,
                    ReviewerName = r.ReviewerName,
                    StarCount = r.StarCount,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            return Ok(reviews);
        }
    }
}
