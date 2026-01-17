using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Dto.Dtos.MovieDtos
{
    public class ResultMovieDetailsDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string CreatedYear { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; } 
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
        public string Director { get; set; } 
        public string Writers { get; set; } 
        public List<string> Categories { get; set; } 
        public List<ResultCastDto> Casts { get; set; }
        public List<ResultReviewDto> Reviews { get; set; }
        public List<ResultMediaDto> Media { get; set; }
        public List<ResultMovieDto> resultMovieDtos { get; set; }

    }

}
