using Microsoft.AspNetCore.Mvc;
using MovieApi.Dto.Dtos.MovieDtos;
using Newtonsoft.Json;

namespace MovieApi.WebApi.Controllers
{
    public class DocumentaryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DocumentaryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> DocumentaryList()
        {
            ViewBag.v1 = "Belgesel Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Belgeseller";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7132/api/Documentaries");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultMovieDto>());

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMovieDto>>(jsonData);

            return View(values);
        }
    }

}
