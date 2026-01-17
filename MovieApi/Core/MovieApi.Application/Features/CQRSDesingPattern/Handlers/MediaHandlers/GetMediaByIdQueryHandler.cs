using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaQueries;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class GetMediaByIdQueryHandler : IRequestHandler<GetMediaByIdQuery, ResultMediaDto?>
    {
        private readonly MovieContext _context;
        public GetMediaByIdQueryHandler(MovieContext context) => _context = context;

        public async Task<ResultMediaDto?> Handle(GetMediaByIdQuery q, CancellationToken ct)
        {
            return await _context.Media
                .Where(m => m.Id == q.Id)
                .Select(m => new ResultMediaDto
                {
                    Id = m.Id,
                    MovieId = m.MovieId,
                    VideoUrl = m.VideoUrl,
                    VideoThumbnail = m.VideoThumbnail,
                    Title = m.Title,
                    Duration = m.Duration
                })
                .FirstOrDefaultAsync(ct);
        }
    }
}
