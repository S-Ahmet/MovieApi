using MovieApi.Application.Features.CQRSDesingPattern.Queries.ReviewQueries;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.ReviewHandlers
{
    public class GetReviewListByMovieIdQueryHandler
    {
        private readonly MovieContext _context;

        public GetReviewListByMovieIdQueryHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<ResultReviewDto>> Handle(GetReviewListByMovieIdQuery query)
        {
            var reviews = await _context.Reviews
                .Where(r => r.MovieId == query.MovieId)
                .Select(r => new ResultReviewDto
                {
                    ReviewID = r.ReviewID,
                    MovieId = r.MovieId,
                    ReviewComment = r.ReviewComment,
                    UserRating = r.UserRating,
                    ReviewDate = r.ReviewDate
                }).ToListAsync();

            return reviews;
        }
    }
}
