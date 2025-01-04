using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using PRDenaCo.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Vouchers.ReceiptVoucher.Queries.LoadReceiptVoucher
{
   public  interface ILoadReceiptVoucherService
    {
        ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

    }
    public class LoadReceiptVoucherService : ILoadReceiptVoucherService
    {
        private readonly IDatabaseContext _context;

        public LoadReceiptVoucherService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            var data = this._context.sp_Voucher_ReceiptLoad(Company_Id, Users_Id, CompanyUsers_Id, CurrentDate, VoucherDate);

            return new ResultDto<OtherVoucherLoadDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

   
    //public class ReceiptAccount
    //{
    //    public int Ledger_Id { get; set; }
    //    public string Ledger_Name { get; set; }

    //}
}
