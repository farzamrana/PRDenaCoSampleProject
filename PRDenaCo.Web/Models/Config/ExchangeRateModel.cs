﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DumiSoft.Web.Models.Config
{
    public class ExchangeRateModel
    {
        [Key]
        public int CurrencyId { set; get; }
        [DisplayName("Symbol")]
        public string Symbol { set; get; }
        [DisplayName("Name")]
        public string Name { set; get; }
        [DisplayName("Subunit")]
        public string Subunit { set; get; }
        [DisplayName("CountryName")]
        public string CountryName { set; get; }
    }
}
