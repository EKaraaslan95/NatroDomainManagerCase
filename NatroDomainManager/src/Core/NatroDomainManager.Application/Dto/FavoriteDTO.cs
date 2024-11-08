using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Dto
{
    public class FavoriteDTO
    {
       
        public int DomainId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
