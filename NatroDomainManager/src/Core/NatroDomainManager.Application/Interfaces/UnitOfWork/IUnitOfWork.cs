using Microsoft.EntityFrameworkCore.Storage;
using NatroDomainManager.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        //IDbContextTransaction : EntityFrameworkCore kütüphanesine ihtiyaç vardır.
        Task<IDbContextTransaction> BeginTransactionAsync();
        public IProductRepository ProductRepository { get; }
        public IInternetNameRepository InternetNameRepository { get; }
    }
}
