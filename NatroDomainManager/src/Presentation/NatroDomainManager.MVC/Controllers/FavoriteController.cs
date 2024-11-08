using Microsoft.AspNetCore.Mvc;
using NatroDomainManager.MVC.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace NatroDomainManager.MVC.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7091/api"; // API base URL

        public FavoriteController( HttpClient httpClient)
        {
            
            _httpClient = httpClient;

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7091")
            };


        }
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] int domainId)
        {
            try
            {
                var jsontoken = HttpContext.Session.GetString("JWTToken");
                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(jsontoken);
                string token = jsonObject["token"];  // token'ı alıyoruz


                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("JWT token is missing or invalid.");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // API'ye istek gönderme
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<DomainInfoMVC>>($"{_apiBaseUrl}/Domain/add-favorite/{domainId}");

                if (response == null || response.Data == null)
                {
                    return NotFound("Domain info not found.");
                }




                return Ok(response);
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Invalid login attempt.");
            }


            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> MyFavorites()
        {
            try
            {
                var jsontoken = HttpContext.Session.GetString("JWTToken");
                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(jsontoken);
                string token = jsonObject["token"];  // token'ı alıyoruz


                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("JWT token is missing or invalid.");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // API'ye istek gönderme
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<DomainInfoMVC>>>($"{_apiBaseUrl}/Domain/my-favorites");

                if (response == null || response.Data == null)
                {
                    return NotFound("List Empty");
                }


                return Ok(response);
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", e.Message);
            }


           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FavoriteInActive([FromBody] FavoriteRequestDto request)
        {
            try
            {
                var jsontoken = HttpContext.Session.GetString("JWTToken");
                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(jsontoken);
                string token = jsonObject["token"];  // token'ı alıyoruz


                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("JWT token is missing or invalid.");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // API'ye istek gönderme
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<DomainInfoMVC>>($"{_apiBaseUrl}/Domain/inactive-favorite/{request.FavoriteId}");

                if (response == null || response.Data == null)
                {
                    return NotFound("Data not found.");
                }




                return Ok(response);
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", e.Message);
            }


            
            return View();

        }



    }

    public class FavoriteRequestDto
    {
        public int FavoriteId { get; set; }
    }

}
