// 📁 MovieApi.Application/Features/CQRSDesingPattern/Handlers/MediaHandlers/GetMediaListByMovieIdQueryHandler.cs
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaQueries;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MediaHandlers
{
    public class GetMediaListByMovieIdQueryHandler
    {
        private readonly MovieContext _context;
        public GetMediaListByMovieIdQueryHandler(MovieContext context) => _context = context;

        public async Task<List<ResultMediaDto>> Handle(GetMediaListByMovieIdQuery q)
        {
            return await _context.Media
                .Where(m => m.MovieId == q.MovieId)
                .Select(m => new ResultMediaDto
                {
                    Id = m.Id,
                    MovieId = m.MovieId,
                    VideoThumbnail = m.VideoThumbnail,
                    VideoUrl = m.VideoUrl,
                    Title = m.Title,
                    Duration = m.Duration,
                    Photos = m.MediaPhotos
                                     .Select(p => new MediaPhotoDto
                                     {
                                         PhotoId = p.Id,
                                         Url = p.PhotoUrl
                                     })
                                     .ToList()
                })
                .ToListAsync();
        }
    }
}
