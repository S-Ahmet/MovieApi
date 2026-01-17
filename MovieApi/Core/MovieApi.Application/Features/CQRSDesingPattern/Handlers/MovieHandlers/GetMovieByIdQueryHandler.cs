using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MovieQeries;
using MovieApi.Application.Features.CQRSDesingPattern.Results.MovieResults;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MovieHandlers
{
    public class GetMovieByIdQueryHandler
    {
        private readonly MovieContext _context;

        public GetMovieByIdQueryHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<GetMovieByIdQueryResult> Handle(GetMovieByIdQuery query)
        {
            var movie = await _context.Movies
                .Include(m => m.CastMovies).ThenInclude(cm => cm.Cast)
                .Include(m => m.Reviews)
                .Include(m => m.Media).ThenInclude(media => media.MediaPhotos)
                .FirstOrDefaultAsync(m => m.MovieId == query.MovieId);

            if (movie == null) return null;

            return new GetMovieByIdQueryResult
            {
                MovieId = movie.MovieId,
                CoverImageUrl = movie.CoverImageUrl,
                CreateDate = movie.CreateDate,
                Description = movie.Description,
                Duration = movie.Duration,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Status = movie.Status,
                Title = movie.Title,
                ReviewCount = movie.Reviews.Count,

                Casts = movie.CastMovies.Select(cm => new ResultCastDto
                {
                    Name = cm.Cast.Name,
                    Surname = cm.Cast.Surname,
                    ImageUrl = cm.Cast.ImageUrl
                }).ToList(),

                Reviews = movie.Reviews.Select(r => new ResultReviewDto
                {
                    ReviewID = r.ReviewID,      // ← Burayı ekledik
                    ReviewComment = r.ReviewComment,
                    UserRating = r.UserRating,
                    ReviewDate = r.ReviewDate,
                    ReviewerName = r.ReviewerName,
                    StarCount = r.StarCount,
                    MovieId = r.MovieId
                }).ToList(),

                Media = movie.Media.Select(m => new ResultMediaDto
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
                                       }).ToList()
                }).ToList()

            };
        }
    }
}
