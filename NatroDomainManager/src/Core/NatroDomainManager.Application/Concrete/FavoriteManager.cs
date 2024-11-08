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
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteRepository _repository;
        private readonly IUserService _userService;

        public FavoriteManager(IFavoriteRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }


        public async Task<Result<DomainInfoDto>> FavoriteInActive(int FavoriteId)
        {
            try
            {
                var data = await _repository.GetAsync(x => x.Id == FavoriteId);


                data.IsActive = false;
                await _repository.UpdateAsync(data);
                return new Result<DomainInfoDto>(ResultStatus.Success, "Favorilerden Kaldırıldı");
            }
            catch (Exception)
            {
                return new Result<DomainInfoDto>(ResultStatus.Error, "Hata Oluştu");
            }
        }


        public async Task<Result<DomainInfoDto>> AddFavoriteAsync(FavoriteDTO favoriteInfo)
        {
            try
            {
                var userId = _userService.GetUserByName(favoriteInfo.UserName).Result.Data.Id;
                favoriteInfo.UserId = userId;
                var data = await GetOrCreateFavoriteAsync(favoriteInfo);
                string message = data.Id == 0 ? "Favorilere eklendi" : "Favorilerde zaten mevcut";

                if (data.Id == 0)
                {
                    data = await _repository.AddAsync(data, f => f.InternetName);
                }

                var domainInfo = data.InternetName;
                var domainInfoDto = new DomainInfoDto
                {
                    DomainName = domainInfo.DomainName,
                    DomainRegistered = domainInfo.IsAvailable.ToString(),
                    LastCheckedDate = domainInfo.LastCheckedDate
                };

                return new Result<DomainInfoDto>(ResultStatus.Success, message, domainInfoDto);
            }
            catch (Exception)
            {
                return new Result<DomainInfoDto>(ResultStatus.Error, "Hata Oluştu");
            }
        }

        public async Task<Result<List<DomainInfoDto>>> GetMyFavorites(string userName)
        {
            var userId = _userService.GetUserByName(userName).Result.Data.Id;

            var favorites = await _repository.GetAllAsync(x => x.UserId == userId && x.IsActive, y => y.InternetName, y => y.User); // InternetName ile join edilmiş veriyi çekiyoruz

            var domainInfoList = favorites.Select(f => MapToDomainInfoDto(f.InternetName,f.Id)).ToList();


            return new Result<List<DomainInfoDto>>(ResultStatus.Success, "Liste Oluşturuldu", domainInfoList);
        }






        private async Task<Favorite> GetOrCreateFavoriteAsync(FavoriteDTO favoriteInfo)
        {
            return await _repository.GetAsync(
                m => m.InternetNameId == favoriteInfo.DomainId && m.UserId == favoriteInfo.UserId && m.IsActive,
                f => f.InternetName, f => f.User
            ) ?? new Favorite
            {
                Id = 0,
                InternetNameId = favoriteInfo.DomainId,
                UserId = favoriteInfo.UserId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
        }

        private DomainInfoDto MapToDomainInfoDto(InternetName domainInfo, int  favoriteId)
        {
            return new DomainInfoDto
            {
                DomainId = domainInfo.Id,
                DomainName = domainInfo.DomainName,
                DomainRegistered = domainInfo.IsAvailable.ToString(),
                LastCheckedDate = domainInfo.LastCheckedDate,
                LastCheckedDateStr = domainInfo.LastCheckedDate.ToString("dd.MM.yyyy hh:mm"),
                ExpiryDate = domainInfo.ExpiryDate?.ToString("dd.MM.yyyy hh:mm"),
                FavoriteId = favoriteId
            };
        }
    }



}
