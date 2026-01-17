using MediatR;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class UpdateMediaPhotoCommandHandler
    : IRequestHandler<UpdateMediaPhotoCommand, Unit>
    {
        private readonly MovieContext _ctx;
        public UpdateMediaPhotoCommandHandler(MovieContext ctx) => _ctx = ctx;

        public async Task<Unit> Handle(UpdateMediaPhotoCommand cmd, CancellationToken ct)
        {
            var entity = await _ctx.MediaPhotos.FindAsync(new object[] { cmd.PhotoId }, ct);
            if (entity is null) return Unit.Value;

            entity.PhotoUrl = cmd.PhotoUrl;
            await _ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }

}
