
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Domain.Entities.Companies
{
    public class Company 
    {
        public int Company_Id { get; set; }
        public int Accounts_Id { get; set; }
        public string Company_BusinessName { get; set; }
        public string Company_AliasName { get; set; }
        public string Company_Address { get; set; }
        public string Company_PhoneNo { get; set; }
        public string Company_Fax { get; set; }
        public string Company_Email { get; set; }
        public string Company_Mobile { get; set; }
        public string Company_WebAddress { get; set; }
       // public byte Country { get; set; }
        public byte DateFormats_Id { get; set; }
        public byte Company_DateSeperator { get; set; }
       // public byte TimeZone { get; set; }
        public string Company_PostalCode { get; set; }
        public byte Currency_Id { get; set; }
        public byte Company_TransactionType { get; set; }
        public byte[] Company_Logo { get; set; }
        public string Company_Tax1 { get; set; }
        public string Company_Tax2 { get; set; }
        public string Company_Tax3 { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
    }
}
