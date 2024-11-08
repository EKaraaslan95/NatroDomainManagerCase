using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Persistence.Context;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatroDomainManager.Application.Dto;
using NatroDomainManager.Application.Enums;
using NatroDomainManager.Application.Exceptions;
using NatroDomainManager.Application.Interfaces.Services;

namespace NatroDomainManager.Persistence.Repositories
{

    //IProductRepository : IRepository<Product>
    public class InternetNameRepository:Repository<InternetName>,IInternetNameRepository
    {

        public InternetNameRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }

  
}
