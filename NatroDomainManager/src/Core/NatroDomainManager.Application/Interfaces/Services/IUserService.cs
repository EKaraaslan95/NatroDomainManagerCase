using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Exceptions;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Result<User>> GetUserByName(string userName);
        Task<Result<User>> UserNamePasswordControl(string userName,string password);
        Task<Result<User>> UserCreate(User user);
    }
}
