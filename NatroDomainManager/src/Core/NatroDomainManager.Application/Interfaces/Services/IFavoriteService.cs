using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.Services
{
    public interface IFavoriteService
    {
        Task<Result<DomainInfoDto>> AddFavoriteAsync(FavoriteDTO favoriteInfo);
        Task<Result<DomainInfoDto>> FavoriteInActive(int FavoriteId);
        Task<Result<List<DomainInfoDto>>> GetMyFavorites(string userName);
 
    }
}
