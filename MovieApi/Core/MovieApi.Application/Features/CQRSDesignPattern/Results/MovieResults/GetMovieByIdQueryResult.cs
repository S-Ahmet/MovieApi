﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesignPattern.Results.MovieResults
{
    public class GetMovieByIdQueryResult
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string CoverImageUrl { get; set; }

        public decimal Rating { get; set; }

        public int Despriction { get; set; }
        public int Durtion { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int CreateDate { get; set; }

        public string CreatedYear { get; set; }

        public bool Status { get; set; }


    }
}
