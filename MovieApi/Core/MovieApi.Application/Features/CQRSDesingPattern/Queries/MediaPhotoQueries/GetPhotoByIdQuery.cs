// Queries/MediaPhotoQueries/GetPhotoByIdQuery.cs
using MediatR;
using MovieApi.Dto.Dtos.MovieDtos;

namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.MediaPhotoQueries
{
    public record GetPhotoByIdQuery(int PhotoId) : IRequest<ResultPhotoDto>;
}
