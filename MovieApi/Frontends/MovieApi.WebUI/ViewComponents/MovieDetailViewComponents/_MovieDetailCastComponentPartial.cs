using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto.Dtos.MovieDtos; 
using System.Collections.Generic;

namespace MovieApi.WebUI.ViewComponents.MovieDetailViewComponents
{
    public class _MovieDetailCastComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(ResultMovieDetailsDto dto)
        {
            // sadece Casts listesini gönderiyoruz
            return View(dto.Casts);
        }
    }
}
