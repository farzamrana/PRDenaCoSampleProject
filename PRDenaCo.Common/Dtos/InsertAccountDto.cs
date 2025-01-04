using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Common.Dtos
{
    public class InsertAccountDto
    {
        public int Users_Id { set; get; }
        public int Accounts_Id { set; get; }
        public byte Error { set; get; }
    }
}
