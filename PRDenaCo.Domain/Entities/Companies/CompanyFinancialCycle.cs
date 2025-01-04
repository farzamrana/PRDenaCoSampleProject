using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Companies
{
    public class CompanyFinancialCycle
    {
        public int FinancialCycle_Id { get; set; }
        public int Company_Id { get; set; }
        public string FinancialCycle_Title { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }
        public bool FinancialCycle_isActive { get; set; }
    }
}
