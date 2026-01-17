using MediatR;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands;
using MovieApi.Domain.Entities;
using MovieApi.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{

    public class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, Unit>
    {
        private readonly MovieContext _context;
        public CreateMediaCommandHandler(MovieContext context) => _context = context;

        public async Task<Unit> Handle(CreateMediaCommand cmd, CancellationToken cancellationToken)
        {
            var entity = new Media
            {
                MovieId = cmd.MovieId,
                VideoThumbnail = cmd.VideoThumbnail,
                VideoUrl = cmd.VideoUrl,
                Title = cmd.Title,
                Duration = cmd.Duration
            };

            await _context.Media.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;   // MediatR için “void” dönüşü
        }
    }
}
