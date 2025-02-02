﻿using DumiSoft.Common.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DumiSoft.Web.Models.Voucher
{
    public class OtherVoucherModel
    {
        [Key]
        public long Id { set; get; }
        [DisplayName("Voucher No")]
        public string VoucherNo { set; get; }
        [DisplayName("RefNo")]
        public string RefNo { set; get; }
        [DisplayName("RefNo2")]
        public string RefNo2 { set; get; }
        [DisplayName("Currency")]
        public int Currency_Id { set; get; }
        [DisplayName("Date")]
        public string VoucherDate { set; get; }
        [DisplayName("Project")]
        public int Project_Id { set; get; }
        [DisplayName("Branch")]
        public int Branch_Id { set; get; }
        [DisplayName("Public Notes")]
        public string PublicNotes { set; get; }
        [DisplayName("Notes")]
        public string Notes { set; get; }
        [DisplayName("Payment")]
        public int PaymentAccount_Id { set; get; }
        [DisplayName("Receipt")]
        public int ReceiptAccount_Id { set; get; }
       
        public bool ProjectActive { set; get; }
        public bool CostCenterActive { set; get; }
        public InFormAccess InFormAccess { set; get; }
        public List<OtherVoucherContentModel> OtherVoucherContentModel { set; get; }
    }
    public class OtherVoucherContentModel
    {
        [Key]
        public long Id { set; get; }
        [DisplayName("No")]
        public int No { set; get; }
        [DisplayName("Account Ledger")]
        public string AccountLedger_Id { set; get; }
        [DisplayName("Balance")]
        public int Balance { set; get; }

        [DisplayName("Dr/Cr")]
        public string DrCr_Id { set; get; }
        [DisplayName("Amount")]
        [Required(ErrorMessage = "{0} is required")]
        public decimal Amount { set; get; }
        [DisplayName("Currency")]
        public string Currency_Id { set; get; }
        [DisplayName("Exchange Rate")]
        public decimal ExchangeRate { set; get; }
        [DisplayName("Cheque No.")]
        public string ChequeNo { set; get; }
        [DisplayName("Cheque Date")]
        public string ChequeDate { set; get; }
        [DisplayName("Remark")]
        public string Remark { set; get; }
        [DisplayName("Cost Center")]
        public string CostCenter_Id { set; get; }
        [DisplayName("Type")]
        public string Type_Id { set; get; }
        public int RecStatus { set; get; }

        public string Rate_Id { set; get; }
        public decimal ExchangeRateOld { set; get; }
    }
}
