using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MovieQeries;
using MovieApi.Persistence.Context;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MovieHandlers
{
    public class GetTotalMovieCountQueryHandler
    {
        private readonly MovieContext _context;

        public GetTotalMovieCountQueryHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetTotalMovieCountQuery query)
        {
            return await Task.FromResult(_context.Movies.Count());
        }
    }
}
