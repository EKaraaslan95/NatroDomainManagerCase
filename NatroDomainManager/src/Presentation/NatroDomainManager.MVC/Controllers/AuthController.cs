using Microsoft.AspNetCore.Mvc;
using NatroDomainManager.MVC.Models;

namespace NatroDomainManager.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7091")
            };
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            // Kullanıcı oturumunu sonlandırmak için session'daki token'ı sil
            HttpContext.Session.Remove("JWTToken");

            // İsteğe bağlı: Tüm session'ları temizlemek için
            HttpContext.Session.Clear();

            // Kullanıcıyı giriş sayfasına yönlendir
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> LoginSign([FromBody] LoginModelMVC model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", model);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    // Token'ı session veya cookie'ye kaydedebilirsiniz.
                    HttpContext.Session.SetString("JWTToken", token);
                     return Json(new { token });
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Invalid login attempt.");
            }
       

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
    }
}
