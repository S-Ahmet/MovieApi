using MediatR;

namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands
{
    public record RemoveMediaCommand(int Id) : IRequest<Unit>;
}
