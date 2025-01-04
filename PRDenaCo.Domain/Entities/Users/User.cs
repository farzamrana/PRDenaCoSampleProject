
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Users
{
    public class User
    {
        public int Users_Id { get; set; }
        public string Users_UserName { get; set; }
        //public string FullName { get; set; }  
        //public string Email { get; set; }
        public string Users_Password { get; set; }
        public string Users_Description { get; set; }
        /// <summary>
        /// 0 : در انتظار تایید
        /// 1 : تایید شده
        /// </summary>
        public byte Users_Status { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
