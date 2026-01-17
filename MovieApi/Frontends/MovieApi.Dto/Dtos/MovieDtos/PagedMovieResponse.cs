using System.Collections.Generic;

namespace MovieApi.Dto.Dtos.MovieDtos
{
    public class PagedMovieResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalMovies { get; set; }
        public int TotalPages { get; set; }
        public List<ResultMovieDto> Data { get; set; }
    }
}
