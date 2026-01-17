using MediatR;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class RemoveMediaCommandHandler : IRequestHandler<RemoveMediaCommand, Unit>
    {
        private readonly MovieContext _ctx;
        public RemoveMediaCommandHandler(MovieContext ctx) => _ctx = ctx;

        public async Task<Unit> Handle(RemoveMediaCommand cmd, CancellationToken ct)
        {
            var entity = await _ctx.Media.FindAsync(new object[] { cmd.Id }, ct);
            if (entity == null) return Unit.Value;
            _ctx.Media.Remove(entity);
            await _ctx.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
