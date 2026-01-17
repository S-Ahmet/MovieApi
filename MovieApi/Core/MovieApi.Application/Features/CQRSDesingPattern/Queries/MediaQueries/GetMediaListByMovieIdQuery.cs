using MediatR;
using MovieApi.Dto.Dtos.MovieDtos;

namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaQueries
{
    public record GetMediaListByMovieIdQuery(int MovieId)
        : IRequest<List<ResultMediaDto>>;
}
