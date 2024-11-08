using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NatroDomainManager.Application.Concrete;
using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Application.Interfaces.Services;
using NatroDomainManager.Application.Interfaces.UnitOfWork;
using NatroDomainManager.Domain.Entities;
using NatroDomainManager.Persistence.Context;
using NatroDomainManager.Persistence.Repositories;
using NatroDomainManager.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration?.GetConnectionString("SQLConnection")));
           

           // serviceCollection.AddTransient<Application.Interfaces.Repositories.IInternetNameService, InternetNameRepository>();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            serviceCollection.AddTransient<IInternetNameRepository, InternetNameRepository>();
            serviceCollection.AddTransient<IFavoriteRepository, FavoriteRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddTransient<IInternetNameService, InternetNameManager>();
            serviceCollection.AddTransient<IFavoriteService, FavoriteManager>();
            serviceCollection.AddTransient<IUserService, UserManager>();
            serviceCollection.AddTransient<IAuthService, AuthManager>();

        }
    }
}
