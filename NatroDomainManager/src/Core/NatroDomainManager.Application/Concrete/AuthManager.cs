using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthManager(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public string GenerateJwtToken(string username)
        {

            var userId = _userService.GetUserByName(username).Result.Data.Id;





            var claims = new[]
                {
               new Claim(ClaimTypes.Name, username),
                    new Claim("sub",userId.ToString())  // Kullanıcı ID'si
                };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
