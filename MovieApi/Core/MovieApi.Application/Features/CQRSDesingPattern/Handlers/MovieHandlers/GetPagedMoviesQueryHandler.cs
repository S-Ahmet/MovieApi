using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.Application.Features.CQRSDesingPattern.Queries.MovieQeries;
using MovieApi.Persistence.Context;
using MovieApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Application.Features.CQRSDesingPattern.Handlers.MovieHandlers
{
    public class GetPagedMoviesQueryHandler
    {
        private readonly MovieContext _context;

        public GetPagedMoviesQueryHandler(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> Handle(GetPagedMoviesQuery query)
        {
            var movies = _context.Movies
                .Include(x => x.CategoryMovies)
                    .ThenInclude(cm => cm.Category)
                .AsQueryable();

            // 🎯 1️⃣ Film adı filtreleme
            if (!string.IsNullOrEmpty(query.SearchTitle))
                movies = movies.Where(m => m.Title.Contains(query.SearchTitle));

            // 🎯 2️⃣ Tür filtreleme
            if (!string.IsNullOrEmpty(query.Genre))
                movies = movies.Where(m => m.CategoryMovies.Any(c => c.Category.CategoryName.Contains(query.Genre)));

            // 🎯 3️⃣ Reyting aralığı
            movies = movies.Where(m => m.Rating >= query.MinRating && m.Rating <= query.MaxRating);

            // 🎯 4️⃣ Yıl aralığı
            if (query.YearFrom.HasValue)
                movies = movies.Where(m => m.ReleaseDate.Year >= query.YearFrom.Value);

            if (query.YearTo.HasValue)
                movies = movies.Where(m => m.ReleaseDate.Year <= query.YearTo.Value);

            // 🔽 5️⃣ Sıralama kriteri (senin haliyle aynı)
            switch ((query.SortBy ?? "rating").ToLower())
            {
                case "title":
                    movies = query.Desc
                        ? movies.OrderByDescending(x => x.Title)
                        : movies.OrderBy(x => x.Title);
                    break;

                case "date":
                    movies = query.Desc
                        ? movies.OrderByDescending(x => x.ReleaseDate)
                        : movies.OrderBy(x => x.ReleaseDate);
                    break;

                case "rating":
                default:
                    movies = query.Desc
                        ? movies.OrderByDescending(x => x.Rating)
                        : movies.OrderBy(x => x.Rating);
                    break;
            }

            // 🔹 Sayfalama
            movies = movies
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking();

            return await movies.ToListAsync();
        }
    }
}
