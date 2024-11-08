using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Exceptions;
using NatroDomainManager.Application.Interfaces.Services;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.Services
{
    public interface  IInternetNameService
    {         
        Task<Result<DomainInfoDto>> AddorUpdateAsync(DomainInfoDto domainInfo);
    }
 

}
