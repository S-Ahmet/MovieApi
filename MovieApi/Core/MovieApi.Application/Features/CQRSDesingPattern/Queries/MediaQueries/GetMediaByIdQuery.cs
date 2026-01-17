using MediatR;
using MovieApi.Dto.Dtos.MovieDtos;   // ResultMediaDto burada yer alıyorsa

namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaQueries
{

    public record GetMediaByIdQuery(int Id) : IRequest<ResultMediaDto>;
}
