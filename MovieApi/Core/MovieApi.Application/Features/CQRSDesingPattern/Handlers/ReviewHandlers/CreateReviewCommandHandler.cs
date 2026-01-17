using MovieApi.Application.Features.CQRSDesingPattern.Commands.ReviewCommands;
using MovieApi.Domain.Entities;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.ReviewHandlers
{
    public class CreateReviewCommandHandler
    {
        private readonly MovieContext _context;

        public CreateReviewCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateReviewCommand command)
        {
            var review = new Review
            {
                MovieId = command.MovieId,
                ReviewComment = command.ReviewComment,
                UserRating = command.UserRating,
                ReviewDate = command.ReviewDate,
                ReviewerName = command.ReviewerName,
                StarCount = command.StarCount,
                Status = command.Status
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}
