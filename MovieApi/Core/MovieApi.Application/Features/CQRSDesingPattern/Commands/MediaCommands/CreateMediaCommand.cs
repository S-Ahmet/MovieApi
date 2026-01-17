using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaCommands
{
    public class CreateMediaCommand : IRequest<Unit>
    {
        public int MovieId { get; set; }
        public string VideoUrl { get; set; }
        public string VideoThumbnail { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
    }
}
