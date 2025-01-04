using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Users
{
    public class Accounts
    {
        public int Accounts_Id { get; set; }
        public string Accounts_Title { get; set; }
        public int Users_Id { get; set; }
        public byte Accounts_Status { get; set; }
    }
}
