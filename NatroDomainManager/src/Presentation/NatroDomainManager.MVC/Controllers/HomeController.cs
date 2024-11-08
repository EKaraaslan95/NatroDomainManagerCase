using Microsoft.AspNetCore.Mvc;
using NatroDomainManager.MVC.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace NatroDomainManager.MVC.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7091/api"; // API base URL
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
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

      



        [HttpPost]///Auth/LoginSign
        public async Task<IActionResult> DomainSearch([FromBody] string domainName)
        {
            try
            {
                var jsontoken = HttpContext.Session.GetString("JWTToken");
                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(jsontoken);
                string token = jsonObject["token"];  // token'ý alýyoruz


                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized("JWT token is missing or invalid.");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // API'ye istek gönderme
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<DomainInfoMVC>>($"{_apiBaseUrl}/Domain/name-control/{domainName}");

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

        //public async Task<IActionResult> GetData()
        //{
        //    var token = HttpContext.Session.GetString("JWTToken");

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return Unauthorized();
        //    }

        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7091/api/data");
        //    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var response = await _httpClient.SendAsync(requestMessage);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var data = await response.Content.ReadAsStringAsync();
        //        return View(model: data);
        //    }

        //    return Unauthorized();
        //}
    }
}
