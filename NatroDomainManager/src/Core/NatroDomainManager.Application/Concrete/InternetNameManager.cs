using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Enums;
using NatroDomainManager.Application.Exceptions;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Application.Interfaces.Services;
using NatroDomainManager.Application.Interfaces.UnitOfWork;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Concrete
{



    public class InternetNameManager : IInternetNameService
    {
        private readonly IInternetNameRepository _repository;
        public InternetNameManager(IInternetNameRepository repository)
        {
            _repository = repository;
        }


        public async Task<Result<DomainInfoDto>> AddorUpdateAsync(DomainInfoDto domainInfo)
        {
            try
            {                

                var data = await _repository.GetAsync(m => m.DomainName == domainInfo.DomainName)?? new InternetName
                {
                    Id = 0,
                    DomainName = domainInfo.DomainName,
                    ExpiryDate = DateTime.TryParse(domainInfo.ExpiryDate, out var expiryDate)
                  ? expiryDate
                  : (DateTime?)null,
                    IsAvailable = domainInfo.DomainRegistered=="no"?true:false,
                    LastCheckedDate = DateTime.Now
                };
                DateTime prevLastCheckedDate = data.LastCheckedDate;
                if (data.Id == 0)
                {

                    data = await _repository.AddAsync(data);

                }
                else
                {
                    data.ExpiryDate = DateTime.TryParse(domainInfo.ExpiryDate, out var expiryDatennew)
     ? (DateTime?)expiryDatennew
     : null;
                    data.IsAvailable = domainInfo.DomainRegistered == "no" ? true : false;
                    data.LastCheckedDate = DateTime.Now;

                    // Mevcut kaydı güncelle                  
                     data = await _repository.UpdateAsync(data)!; // UpdateAsync metodu var ise güncelle

                }

                return  new Result<DomainInfoDto>(ResultStatus.Success, "Kayıt Başarılı", new DomainInfoDto
                {
                    DomainId=data.Id,
                    DomainName = domainInfo.DomainName,
                    ExpiryDate = domainInfo.ExpiryDate,
                    CreateDate=domainInfo.CreateDate,
                    UpdateDate=domainInfo.UpdateDate,
                    DomainRegistered = data.IsAvailable.ToString(),
                    LastCheckedDate = prevLastCheckedDate
                });
            }
            catch (Exception)
            {

                return  new Result<DomainInfoDto>(ResultStatus.Error, "Hata Oluştu");
            }
        }
    }
}
