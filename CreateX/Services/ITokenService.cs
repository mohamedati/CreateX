using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Createx.Core.Entities;
namespace Services.Services
{
    public  interface  ITokenService
    {
       string GenereateToken(ApplicationUser user);

       string GenereateRefreshToken();
    }
}
