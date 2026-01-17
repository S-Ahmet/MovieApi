using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class RemoveMediaPhotoCommandHandler
    {
        private readonly MovieContext _context;
        public RemoveMediaPhotoCommandHandler(MovieContext context) => _context = context;

        public async Task Handle(RemoveMediaPhotoCommand cmd)
        {
            var photo = await _context.MediaPhotos.FindAsync(cmd.PhotoId);
            if (photo != null)
            {
                _context.MediaPhotos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
