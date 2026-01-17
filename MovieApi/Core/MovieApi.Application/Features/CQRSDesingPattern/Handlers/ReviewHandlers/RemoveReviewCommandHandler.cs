using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieApi.Persistence.Context;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.ReviewCommands;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.ReviewHandlers
{
    public class RemoveReviewCommandHandler
    {
        private readonly MovieContext _context;

        public RemoveReviewCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveReviewCommand command)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewID == command.ReviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
