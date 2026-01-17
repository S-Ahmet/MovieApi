// Handlers/MediaPhotoHandlers/UpdateMediaPhotoCommandHandler.cs
using MediatR;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaPhotoHandlers
{
    public class UpdateMediaPhotoCommandHandler
        : IRequestHandler<UpdateMediaPhotoCommand, Unit>
    {
        private readonly MovieContext _ctx;
        public UpdateMediaPhotoCommandHandler(MovieContext ctx) => _ctx = ctx;

        public async Task<Unit> Handle(UpdateMediaPhotoCommand c, CancellationToken ct)
        {
            var photo = await _ctx.MediaPhotos.FindAsync(new object?[] { c.PhotoId }, ct);
            if (photo is null) return Unit.Value;

            photo.PhotoUrl = c.PhotoUrl;
            photo.MediaId = c.MediaId;
            await _ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
