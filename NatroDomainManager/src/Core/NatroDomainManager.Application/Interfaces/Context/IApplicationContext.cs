using Microsoft.EntityFrameworkCore;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<InternetName> InternetNames { get; set; }
        DbSet<Favorite> Favorites { get; set; }
        DbSet<User> Users { get; set; }
    }
}
