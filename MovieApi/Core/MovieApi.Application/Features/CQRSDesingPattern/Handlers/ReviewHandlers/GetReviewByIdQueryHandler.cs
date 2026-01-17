using MovieApi.Application.Features.CQRSDesingPattern.Queries.ReviewQueries;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.ReviewHandlers
{
    public class GetReviewByIdQueryHandler
    {
        private readonly MovieContext _context;

        public GetReviewByIdQueryHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<ResultReviewDto?> Handle(GetReviewByIdQuery query)
        {
            var review = await _context.Reviews
                .Where(x => x.ReviewID == query.ReviewId)
                .Select(y => new ResultReviewDto
                {
                    ReviewID = y.ReviewID,
                    MovieId = y.MovieId,
                    ReviewComment = y.ReviewComment,
                    UserRating = y.UserRating,
                    ReviewDate = y.ReviewDate,
                    ReviewerName = y.ReviewerName,
                    StarCount = y.StarCount
                })
                .FirstOrDefaultAsync();

            return review;
        }
    }
}
