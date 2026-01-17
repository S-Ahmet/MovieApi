using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto.Dtos.MovieDtos;

namespace MovieApi.WebUI.ViewComponents.MovieDetailViewComponents
{
    public class _MovieDetailMediaComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(ResultMovieDetailsDto model)
        {
            return View("Default", model);
        }
    }
}
