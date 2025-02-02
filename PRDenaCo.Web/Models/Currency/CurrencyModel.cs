﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Currency
{
    public class CurrencyModel
    {
        [Key]
        public long Currency_Id { set; get; }
        [DisplayName("Name")]
        public string Currency_Name { set; get; }
        [DisplayName("Symbol")]
        public string Currency_Symbol { set; get; }
    }
    public class CurrencyInfoModel : CurrencyModel
    {
        public int Company_Id { set; get; }

        [DisplayName("Sub Currency")]
        public string Currency_Subunit { set; get; }
        public int Beneficiary_Id { get; internal set; }
        public MessageViewModel OprMessage { get; set; }
        public CurrencyInfoModel()
        {
            OprMessage = new MessageViewModel();
            
        }
    }

}
