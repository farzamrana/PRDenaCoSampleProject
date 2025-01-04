
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Companies
{
    public class Currency 
    {
        public int Currency_Id { get; set; }
        public string Currency_Symbol { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Subunit { get; set; }
        public string Currency_CountryName { get; set; }

    }
}
