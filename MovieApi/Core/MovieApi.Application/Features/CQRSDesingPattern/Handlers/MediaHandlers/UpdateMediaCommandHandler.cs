using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands;
using MovieApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class UpdateMediaCommandHandler
    {
        private readonly MovieContext _context;
        public UpdateMediaCommandHandler(MovieContext context) => _context = context;

        public async Task Handle(UpdateMediaCommand cmd)
        {
            var entity = await _context.Media.FirstOrDefaultAsync(m => m.Id == cmd.Id);
            if (entity == null) return;

            entity.MovieId = cmd.MovieId;
            entity.VideoThumbnail = cmd.VideoThumbnail;
            entity.VideoUrl = cmd.VideoUrl;
            entity.Title = cmd.Title;
            entity.Duration = cmd.Duration;
            await _context.SaveChangesAsync();
        }
    }
}
