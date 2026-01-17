using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesingPattern.Commands.ReviewCommands
{
    public class CreateReviewCommand
    {
        public int MovieId { get; set; }
        public string ReviewComment { get; set; }
        public int UserRating { get; set; }
        public DateTime ReviewDate { get; set; }

        public string ReviewerName { get; set; }
        public int StarCount { get; set; }
        public bool Status { get; set; }
    }
}
