﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Account
{
    public class AccountGroupModel
    {
        [Key]
        [DisplayName("Under")]
        public int AccountGroup_Id { get; set; }
        public int Company_Id { get; set; }
        public int Parent { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Narration")]
        public string Narration { get; set; }
        [DisplayName("Nature")]
        public byte Nature_Id { get; set; }
       
        [DisplayName("Affect Gross Profit")]
        public bool GrossProfit { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public AccountGroupModel()
        {
            OprMessage = new MessageViewModel();
          
        }
    }
}
