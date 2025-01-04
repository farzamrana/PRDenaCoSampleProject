using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.UserAccess
{
    public class UserAccessModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
        public int CompanyUsers_MenuId { get; set; }
    }
}
