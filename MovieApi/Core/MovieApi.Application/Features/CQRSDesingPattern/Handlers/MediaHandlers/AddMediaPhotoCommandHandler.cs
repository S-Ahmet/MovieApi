using MediatR;  // ← ekleyin
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Domain.Entities;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    // IRequestHandler<TRequest, TResponse>
    public class AddMediaPhotoCommandHandler : IRequestHandler<AddMediaPhotoCommand, Unit>
    {
        private readonly MovieContext _context;
        public AddMediaPhotoCommandHandler(MovieContext context) => _context = context;

        public async Task<Unit> Handle(AddMediaPhotoCommand cmd, CancellationToken cancellationToken)
        {
            var entity = new MediaPhoto
            {
                MediaId = cmd.MediaId,
                PhotoUrl = cmd.PhotoUrl
            };

            await _context.MediaPhotos.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
