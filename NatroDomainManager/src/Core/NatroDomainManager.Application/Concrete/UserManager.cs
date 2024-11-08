using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Enums;
using NatroDomainManager.Application.Exceptions;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Application.Interfaces.Services;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<User>> GetUserByName(string userName)
        {
            var data = await _repository.GetAsync(x => x.UserName == userName);
            return new Result<User>(ResultStatus.Success, "Data Created", data);

        }

        public async Task<Result<User>> UserCreate(User user)
        {
            throw new NotImplementedException();
            //string hash1234 = BCrypt.Net.BCrypt.HashPassword("1234");
            //string hash12345 = BCrypt.Net.BCrypt.HashPassword("12345");
            //string hash123456 = BCrypt.Net.BCrypt.HashPassword("123456");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234");
            var data = _repository.AddAsync(user);
            return new Result<User>(ResultStatus.Success, "");
        }

        public async Task<Result<User>> UserNamePasswordControl(string userName, string password)
        {
            var data = await _repository.GetAsync(x => x.UserName == userName);
        
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, data.PasswordHash);

            if (!isPasswordValid)
            {
                return new Result<User>(ResultStatus.Error, "Invalid password");
            }

            // Kullanıcı adı ve parola doğruysa, kullanıcıyı geri döndür
            return new Result<User>(ResultStatus.Success, "Login Success", data);
        }
    }
}
