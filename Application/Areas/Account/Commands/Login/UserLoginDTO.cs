using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Areas.Account.Commands.Login
{
    public  class UserLoginDTO
    {
        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;


        public string Token { get; set; } = null;

        public string RefreshToken { get; set; } = null;
    }
}
