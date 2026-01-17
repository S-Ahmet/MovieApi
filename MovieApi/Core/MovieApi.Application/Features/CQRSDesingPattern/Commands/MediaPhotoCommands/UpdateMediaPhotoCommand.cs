using MediatR;


namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands
{
    public record UpdateMediaPhotoCommand(int PhotoId, int MediaId, string PhotoUrl) : IRequest<Unit>;
}