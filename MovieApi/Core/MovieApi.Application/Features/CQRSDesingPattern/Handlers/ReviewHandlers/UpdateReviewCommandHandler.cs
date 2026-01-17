using MovieApi.Application.Features.CQRSDesingPattern.Commands.ReviewCommands;
using MovieApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.ReviewHandlers
{
    public class UpdateReviewCommandHandler
    {
        private readonly MovieContext _context;

        public UpdateReviewCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateReviewCommand command)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.ReviewID == command.ReviewId);
            if (review != null)
            {
                review.ReviewComment = command.ReviewComment;
                review.UserRating = command.UserRating;
                review.ReviewDate = command.ReviewDate;
                review.ReviewerName = command.ReviewerName;
                review.StarCount = command.StarCount;
                review.Status = command.Status;

                await _context.SaveChangesAsync();
            }
        }
    }
}
