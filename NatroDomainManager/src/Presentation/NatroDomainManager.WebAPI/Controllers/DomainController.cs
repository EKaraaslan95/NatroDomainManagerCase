using Microsoft.AspNetCore.Mvc;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Domain.Entities;
using NatroDomainManager.Persistence.Repositories;
using NatroDomainManager.Domain.Entities;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Web;
using Microsoft.Win32;
using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NatroDomainManager.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : Controller
    {
        readonly IInternetNameService _nameService;
        readonly IFavoriteService _favoriteService;
        private readonly HttpClient _httpClient;
        public DomainController(HttpClient httpClient, IInternetNameService nameService, IFavoriteService favoriteService)
        {

            _httpClient = httpClient;
            _nameService = nameService;
            _favoriteService = favoriteService;
        }



        [HttpGet("name-control/{name}")]
        public async Task<IActionResult> NameControl(string name)
        {

            string apiKey = "9ea968050feb40a1bb2f85de859ce2ff"; // API anahtarınızı buraya girin
                                                                //string domainName = "whoisfreaks.com";
            string url = $"https://api.whoisfreaks.com/v1.0/whois?apiKey={apiKey}&whois=live&domainName={name}";

            try
            {
                // GET isteği gönderme
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Başarılı değilse bir istisna fırlatır

                // Yanıt içeriğini okuma
                string responseData = await response.Content.ReadAsStringAsync();
                Root rootData = JsonSerializer.Deserialize<Root>(responseData);

                // İlgili alanları modelimize çekiyoruz
                DomainInfoDto domainInfo = new DomainInfoDto
                {
                    
                    DomainName = rootData.DomainName,
                    QueryTime = rootData.QueryTime,
                    DomainRegistered = rootData.DomainRegistered,
                    CreateDate = rootData.RegistryData?.CreateDate,
                    UpdateDate = rootData.RegistryData?.UpdateDate,
                    ExpiryDate = rootData.RegistryData?.ExpiryDate
                };


                return Ok(await _nameService.AddorUpdateAsync(domainInfo));
            }
            catch (Exception ex)
            {
                // Hata mesajı ile birlikte 500 Internal Server Error döner
                return StatusCode(500, $"Hata: {ex.Message}");
            }


        }

        [HttpGet("add-favorite/{domainId}")]
        public async Task<IActionResult> AddFavorite(int domainId)
        {

            try
            {
                var username = User.Identity.Name;

                FavoriteDTO favoriteInfo = new FavoriteDTO
                {
                    DomainId = domainId,
                    UserId = 0,// int.Parse(userId)
                    UserName=username
                };


                return Ok(await _favoriteService.AddFavoriteAsync(favoriteInfo));
            }
            catch (Exception ex)
            {
                // Hata mesajı ile birlikte 500 Internal Server Error döner
                return StatusCode(500, $"Hata: {ex.Message}");
            }


        }

        [HttpGet("inactive-favorite/{favoriteId}")]
        public async Task<IActionResult> InActiveFavorite(int favoriteId)
        {

            try
            {
               
               


                return Ok(await _favoriteService.FavoriteInActive(favoriteId));
            }
            catch (Exception ex)
            {
                // Hata mesajı ile birlikte 500 Internal Server Error döner
                return StatusCode(500, $"Hata: {ex.Message}");
            }


        }






        [HttpGet("my-favorites")]
        public async Task<IActionResult> MyFavorites()
        {


            try
            {

                var username = User.Identity.Name;
                return Ok(await _favoriteService.GetMyFavorites(username));
            }
            catch (Exception ex)
            {
                // Hata mesajı ile birlikte 500 Internal Server Error döner
                return StatusCode(500, $"Hata: {ex.Message}");
            }


        }


        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            return Ok(new { message = "Bu korumalı veridir." });
        }

    }

    public class WhoISParsed
    {
        public string name { get; set; }
        public object created { get; set; }
        public object changed { get; set; }
        public object expires { get; set; }
        public object dnssec { get; set; }
        public object registered { get; set; }
        public object status { get; set; }
        public IList<object> nameservers { get; set; }
        public object contacts { get; set; }
        public object throttled { get; set; }
    }
}
