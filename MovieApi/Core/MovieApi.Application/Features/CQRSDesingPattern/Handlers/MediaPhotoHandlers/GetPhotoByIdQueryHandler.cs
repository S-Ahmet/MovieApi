// Handlers/MediaPhotoHandlers/GetPhotoByIdQueryHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaPhotoQueries;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaPhotoHandlers
{
    public class GetPhotoByIdQueryHandler
        : IRequestHandler<GetPhotoByIdQuery, ResultPhotoDto?>
    {
        private readonly MovieContext _ctx;
        public GetPhotoByIdQueryHandler(MovieContext ctx) => _ctx = ctx;

        public async Task<ResultPhotoDto?> Handle(GetPhotoByIdQuery q, CancellationToken ct)
        {
            return await _ctx.MediaPhotos
                .Where(p => p.Id == q.PhotoId)
                .Select(p => new ResultPhotoDto
                {
                    PhotoId = p.Id,
                    MediaId = p.MediaId,
                    Url = p.PhotoUrl
                })
                .FirstOrDefaultAsync(ct);
        }
    }
}
