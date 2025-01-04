using PRDenaCo.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Users
{
    public class UserInformations:User
    {
       // public int Users_Id { get; set; }
        public int Accounts_Id { get; set; }
       public List<UserBusiness> UserInBusiness { get; set; }
        public List<Menu> Menus { get; set; }
       public byte Company_TransactionType { get; set; }
        public byte DateFormats_Id { get; set; }
        public byte Company_DateSeperator { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }
       

    }
    
}
