using Microsoft.AspNetCore.Mvc;
using MovieApi.Application.Features.CQRSDesingPattern.Commands.MediaPhotoCommands;
using MovieApi.Dto.Dtos.MovieDtos;
using MovieApi.Dto.Dtos.ReviewDtos;
using Newtonsoft.Json;
using System.Text;



namespace MovieApi.WebUI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Film listesi
        public async Task<IActionResult> MovieList(
            int page = 1,
            int pageSize = 5,
            string sortBy = "rating",
            bool desc = true,
            string searchTitle = "",
            string genre = "",
            int? minRating = 0,
            int? maxRating = 10,
            int? yearFrom = null,
            int? yearTo = null)
        {
            ViewBag.v1 = "Film Listesi";
            ViewBag.v2 = "Ana Sayfa";
            ViewBag.v3 = "Tüm Filmler";

            var client = _httpClientFactory.CreateClient();

            // 🔹 API isteği — filtreler dahil
            string url = $"https://localhost:7132/api/Movies/paged?" +
                         $"page={page}&pageSize={pageSize}&sortBy={sortBy}&desc={desc}" +
                         $"&searchTitle={searchTitle}&genre={genre}" +
                         $"&minRating={minRating}&maxRating={maxRating}" +
                         $"&yearFrom={yearFrom}&yearTo={yearTo}";

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.TotalMovies = 0;
                return View(new List<ResultMovieDto>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedMovieResponse>(json);

            if (result == null || result.Data == null)
                throw new Exception($"Deserialize null döndü! JSON verisi: {json}");

            // 🎯 ViewBag verilerini set et
            ViewBag.TotalMovies = result.TotalMovies;
            ViewBag.TotalPages = result.TotalPages;
            ViewBag.CurrentPage = result.Page;
            ViewBag.PageSize = result.PageSize;
            ViewBag.SortBy = sortBy;
            ViewBag.Desc = desc;
            ViewBag.SearchTitle = searchTitle;
            ViewBag.Genre = genre;
            ViewBag.MinRating = minRating;
            ViewBag.MaxRating = maxRating;
            ViewBag.YearFrom = yearFrom;
            ViewBag.YearTo = yearTo;

            return View(result.Data);
        }





        public async Task<IActionResult> MovieGrid(int page = 1, int pageSize = 5, string sortBy = "rating", bool desc = true)
        {
            return await MovieList(page, pageSize, sortBy, desc);
        }




        // Film detay
        public async Task<IActionResult> MovieDetail(int id)
        {
            ViewBag.v1 = "Film Listesi";
            ViewBag.v2 = "Ana Sayfa";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Movies/GetMovie?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultMovieDetailsDto>(jsonData);
                ViewBag.v3 = values.Title;

                // Benzer filmler
                var relatedResponse = await client.GetAsync("https://localhost:7132/api/Movies");
                if (relatedResponse.IsSuccessStatusCode)
                {
                    var relatedJson = await relatedResponse.Content.ReadAsStringAsync();
                    var relatedMovies = JsonConvert.DeserializeObject<List<ResultMovieDto>>(relatedJson);
                    values.resultMovieDtos = relatedMovies.Where(x => x.MovieId != values.MovieId).ToList();
                }

                return View(values);
            }

            return View();
        }

        // GET: Yorum yaz sayfasını aç
        [HttpGet]
        public IActionResult AddReview(int movieId)
        {
            var dto = new CreateReviewDto
            {
                MovieId = movieId
            };

            return View(dto); // AddReview.cshtml sayfasını gösterir
        }

        // POST: Yorumu gönder
        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7132/api/Reviews", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieDetail", new { id = dto.MovieId });
            }

            // Hata varsa aynı sayfaya geri dön
            return View(dto);
        }

        // POST: Yorumu sil
        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7132/api/Reviews/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieDetail", new { id = movieId });
            }

            TempData["Error"] = "Yorum silinemedi. API hata verdi.";
            return RedirectToAction("MovieDetail", new { id = movieId });
        }

        // GET: Video Ekleme sayfasını aç
        [HttpGet]
        public IActionResult AddVideo(int movieId)
        {
            ViewBag.MovieId = movieId;
            return View();
        }

        // POST: Video Ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> AddVideo(int MovieId, string VideoUrl, string VideoThumbnail, string Title, string Duration)
        {
            var client = _httpClientFactory.CreateClient();

            var videoDto = new
            {
                MovieId = MovieId,
                VideoUrl = VideoUrl,
                VideoThumbnail = VideoThumbnail,
                Title = Title,
                Duration = Duration
            };

            var jsonData = JsonConvert.SerializeObject(videoDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API endpoint adresini kendi API'ne göre ayarla
            var response = await client.PostAsync("https://localhost:7132/api/Media", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieDetail", new { id = MovieId });
            }

            // Başarısız olursa aynı sayfaya geri dön
            ViewBag.MovieId = MovieId;
            ViewBag.ErrorMessage = "Video eklenirken hata oluştu.";
            return View();
        }
        [HttpGet]
        public IActionResult AddPhoto(int mediaId, int movieId)
        {
            ViewBag.MediaId = mediaId;
            ViewBag.MovieId = movieId;
            // Model binding için komut DTO’sunu başlatmak faydalı:
            var cmd = new AddMediaPhotoCommand { MediaId = mediaId };
            return View(cmd);
        }

        // POST: /Movie/AddPhoto
        [HttpPost]
        public async Task<IActionResult> AddPhoto(AddMediaPhotoCommand cmd, int movieId)
        {
            // cmd.MediaId ve cmd.PhotoUrl doğru geliyor mu?
            // istersen burada da loglayabilir veya ModelState’i kontrol edebilirsin

            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(cmd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7132/api/MediaPhotos", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("MovieDetail", new { id = movieId });

            // hata varsa hata mesajını ModelState’e ekle
            ModelState.AddModelError("", "Fotoğraf eklenirken hata oluştu.");
            ViewBag.MediaId = cmd.MediaId;
            ViewBag.MovieId = movieId;
            return View(cmd);
        }
        [HttpGet]
        public async Task<IActionResult> EditVideo(int id, int movieId)
        {
            // id: düzenlenecek Media (video) kaydının Id’si
            var client = _httpClientFactory.CreateClient();
            var resp = await client.GetAsync($"https://localhost:7132/api/Media/{id}");
            if (!resp.IsSuccessStatusCode) return NotFound();

            var json = await resp.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ResultMediaDto>(json);

            ViewBag.MovieId = movieId;          // İptal butonu için lazım
            return View(dto);                   // Views/Movie/EditVideo.cshtml
        }


        // 2)  POST  /Movie/EditVideo
        [HttpPost]
        public async Task<IActionResult> EditVideo(ResultMediaDto dto, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"https://localhost:7132/api/Media/{dto.Id}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction("MovieDetail", new { id = movieId });

            ModelState.AddModelError("", "Video güncellenemedi.");
            ViewBag.MovieId = movieId;
            return View(dto);                   // Hata varsa formu yeniden göster
        }
        // MovieController.cs

        // … AddVideo, EditVideo aksiyonlarının PEŞİNE ekle
        [HttpPost]
        public async Task<IActionResult> DeleteVideo(int id, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            var resp = await client.DeleteAsync($"https://localhost:7132/api/Media/{id}");

            if (resp.IsSuccessStatusCode)
                return RedirectToAction("MovieDetail", new { id = movieId });

            TempData["Error"] = "Video silinemedi. API hata verdi.";
            return RedirectToAction("MovieDetail", new { id = movieId });

        }
        // GET  /Movie/EditPhoto/{id}?movieId=5
        [HttpGet]
        public async Task<IActionResult> EditPhoto(int id, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            var resp = await client.GetAsync($"https://localhost:7132/api/MediaPhotos/{id}");
            if (!resp.IsSuccessStatusCode) return NotFound();

            var dto = JsonConvert.DeserializeObject<ResultPhotoDto>(await resp.Content.ReadAsStringAsync());
            ViewBag.MovieId = movieId;
            return View(dto);          // Views/Movie/EditPhoto.cshtml
        }

        // POST  /Movie/EditPhoto
        [HttpPost]
        public async Task<IActionResult> EditPhoto(ResultPhotoDto dto, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(
                new { dto.PhotoId, dto.MediaId, PhotoUrl = dto.Url });   // Update cmd'ine uyar

            var resp = await client.PutAsync($"https://localhost:7132/api/MediaPhotos/{dto.PhotoId}",
                                             new StringContent(json, Encoding.UTF8, "application/json"));

            if (resp.IsSuccessStatusCode)
                return RedirectToAction("MovieDetail", new { id = movieId });

            ModelState.AddModelError("", "Fotoğraf güncellenemedi");
            ViewBag.MovieId = movieId;
            return View(dto);
        }

        // POST  /Movie/DeletePhoto
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, int movieId)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7132/api/MediaPhotos/{id}");
            return RedirectToAction("MovieDetail", new { id = movieId });
        }




    }
}