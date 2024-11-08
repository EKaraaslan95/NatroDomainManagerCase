using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Enums;
using NatroDomainManager.Application.Interfaces.Services;

namespace NatroDomainManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {

            var control=_userService.UserNamePasswordControl(model.Username, model.Password).Result.ResultStatus;
            // Burada kullanıcı adı ve şifreyi kontrol edin
            if (control==ResultStatus.Success)
            {
                // Token oluştur
                var token = _authService.GenerateJwtToken(model.Username);
                return Ok(new { token });
            }
            return Unauthorized();

        }
    }
}
