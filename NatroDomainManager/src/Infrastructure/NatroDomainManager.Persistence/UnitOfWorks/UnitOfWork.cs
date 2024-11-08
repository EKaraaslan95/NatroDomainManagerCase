using Microsoft.EntityFrameworkCore.Storage;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Application.Interfaces.Services;
using NatroDomainManager.Application.Interfaces.UnitOfWork;
using NatroDomainManager.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository ProductRepository { get; }

        public IInternetNameRepository InternetNameRepository { get; }
        public IFavoriteRepository FavoriteRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository,IInternetNameRepository internetNameRepository, IFavoriteRepository favoriteRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            InternetNameRepository = internetNameRepository;
            FavoriteRepository = favoriteRepository;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        public async ValueTask DisposeAsync() { }
    }
}
