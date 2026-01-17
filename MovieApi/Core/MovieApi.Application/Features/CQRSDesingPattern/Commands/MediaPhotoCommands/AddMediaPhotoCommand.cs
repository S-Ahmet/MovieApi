using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 // ← ekleyin

namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands
{
    // IRequest<Unit> diyerek Unit (void) tipi döndürüyoruz
    public class AddMediaPhotoCommand : IRequest<Unit>
    {
        public int MediaId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
