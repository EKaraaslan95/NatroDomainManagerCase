using NatroDomainManager.Application.Interfaces.Repositories;
using NatroDomainManager.Domain.Entities;
using NatroDomainManager.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Persistence.Repositories
{
    

    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {

        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
