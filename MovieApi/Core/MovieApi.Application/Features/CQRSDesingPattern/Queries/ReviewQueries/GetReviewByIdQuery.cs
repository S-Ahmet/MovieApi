using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.ReviewQueries
{
    public class GetReviewByIdQuery
    {
        public int ReviewId { get; set; }

        public GetReviewByIdQuery(int reviewId)
        {
            ReviewId = reviewId;
        }
    }
}
