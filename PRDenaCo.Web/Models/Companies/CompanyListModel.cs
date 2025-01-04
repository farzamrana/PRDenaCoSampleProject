using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Companies
{
    public class CompanyListModel
    {
        [Key]
        public int CompanyId { get; set; }
        [DisplayName("Bussiness Name")]
        public string BusinessName { get; set; }
        [DisplayName("Alias Name")]
        public string AliasName { get; set; }
        public byte TransactionType { get; set; }
    }
}
