﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Models.Voucher
{
    public class VoucherDialogModel
    {
        [Key]
        public long Id { set; get; }
        public int voucherType { set; get; }
        public List< VoucherListModel> VoucherListModel { set; get; }
      

    }
    public class VoucherListModel
    {
        [Key]
        public long Id { set; get; }
        [DisplayName("Voucher No")]
        public string VoucherNo { set; get; }
        [DisplayName("Date")]
        public string VoucherDate { set; get; }
       
    }
}
