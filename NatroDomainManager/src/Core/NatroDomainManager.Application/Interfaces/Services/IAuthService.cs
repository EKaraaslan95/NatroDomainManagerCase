using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
    }
}
