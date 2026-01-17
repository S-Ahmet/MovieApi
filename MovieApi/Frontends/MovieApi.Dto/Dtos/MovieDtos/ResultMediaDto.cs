using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Dto.Dtos.MovieDtos
{
    public class ResultMediaDto
    {
        public int Id { get; set; }                
        public int MovieId { get; set; }
        public string VideoThumbnail { get; set; }
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public List<MediaPhotoDto> Photos { get; set; }

    }

}
