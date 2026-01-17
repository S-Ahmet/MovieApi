using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MovieApi.Application.Features.CQRSDesingPattern.Queries.MovieQeries
{
    public class GetPagedMoviesQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool Desc { get; set; }

        // 🔍 Ek filtre özellikleri
        public string SearchTitle { get; set; }      // Film adı arama
        public string Genre { get; set; }            // Tür arama
        public int MinRating { get; set; } = 0;      // Minimum reyting
        public int MaxRating { get; set; } = 10;     // Maksimum reyting
        public int? YearFrom { get; set; }           // Başlangıç yılı
        public int? YearTo { get; set; }             // Bitiş yılı

        public GetPagedMoviesQuery(int page, int pageSize, string sortBy, bool desc)
        {
            Page = page;
            PageSize = pageSize;
            SortBy = sortBy;
            Desc = desc;
        }
    }
}
