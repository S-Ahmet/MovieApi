using MediatR;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaPhotoHandlers
{
    public class RemoveMediaPhotoCommandHandler
           : IRequestHandler<RemoveMediaPhotoCommand, Unit>
    {
        private readonly MovieContext _context;
        public RemoveMediaPhotoCommandHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveMediaPhotoCommand request,
                                       CancellationToken cancellationToken)
        {
            var entity = await _context.MediaPhotos.FindAsync(request.PhotoId);
            if (entity is null) return Unit.Value;

            _context.MediaPhotos.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
