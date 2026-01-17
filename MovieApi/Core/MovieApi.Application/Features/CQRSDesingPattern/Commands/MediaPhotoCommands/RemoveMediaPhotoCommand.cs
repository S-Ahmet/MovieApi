// Features/CQRSDesingPattern/Commands/MediaPhotoCommands/RemoveMediaPhotoCommand.cs
using MediatR;

namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands
{
    // Unit = geriye void döner gibi
    public class RemoveMediaPhotoCommand : IRequest<Unit>
    {
        public int PhotoId { get; }

        public RemoveMediaPhotoCommand(int photoId)
        {
            PhotoId = photoId;
        }
    }
}
