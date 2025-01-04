using PRDenaCo.Application.Services.Users.Commands.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Utilities
{
    public class ActiveUser: ResultUserloginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
