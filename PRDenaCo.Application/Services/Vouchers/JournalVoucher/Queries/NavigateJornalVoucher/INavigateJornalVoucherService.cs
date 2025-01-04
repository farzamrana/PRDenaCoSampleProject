using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher
{
    public interface INavigateJornalVoucherService
    {
        ResultDto<JournalVoucherDto> Execute(byte VoucherType_Id, long CurrentvoucherMasters_Id, byte Navigate_Status);
    }
    public class NavigateJornalVoucherService : INavigateJornalVoucherService
    {
        private readonly IDatabaseContext _context;

        public NavigateJornalVoucherService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<JournalVoucherDto> Execute(byte VoucherType_Id, long CurrentvoucherMasters_Id, byte Navigate_Status)
        {
            byte Error;
            var data = this._context.sp_Voucher_Navigate( VoucherType_Id,  CurrentvoucherMasters_Id,  Navigate_Status,out Error);
            string message = AppMessages.SUCCESS;
            switch (Error)
            {
                case 0:
                    message = AppMessages.SUCCESS;
                    return new ResultDto<JournalVoucherDto>()
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = message,
                    };
                case 1:
                    message = AppMessages.VOUCHER_NO_DATA;
                    break;
                case 2:
                    message = AppMessages.VOUCHER_SAME_RECORD;
                    break;

            }
           

            return new ResultDto<JournalVoucherDto>()
            {
                IsSuccess = false,
                Message = message,
            };
        }
    }
}
