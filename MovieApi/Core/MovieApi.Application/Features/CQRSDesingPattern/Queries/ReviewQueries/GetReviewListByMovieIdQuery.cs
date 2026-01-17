using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.ReviewQueries
{
    public class GetReviewListByMovieIdQuery
    {
        public int MovieId { get; set; }

        public GetReviewListByMovieIdQuery(int movieId)
        {
            MovieId = movieId;
        }
    }
}
