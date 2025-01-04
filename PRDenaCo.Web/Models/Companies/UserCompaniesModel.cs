using PRDenaCo.Application.Services.Users.Commands.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Companies
{
    public class UserCompaniesModel
    {
        public int CompanyUsers_Id { get; set; }
        public List<UserBusinessDto> UserBusiness { get; set; }
    }
}
