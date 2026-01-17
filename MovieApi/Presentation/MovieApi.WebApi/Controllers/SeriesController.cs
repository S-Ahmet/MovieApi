using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto.Dtos.MovieDtos;
using Newtonsoft.Json;

namespace MovieApi.WebUI.Controllers
{
    public class SeriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SeriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> SeriesList()
        {
            ViewBag.v1 = "Dizi Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Diziler";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7132/api/Series");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultMovieDto>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMovieDto>>(jsonData);

            return View(values);
        }
    }
}
