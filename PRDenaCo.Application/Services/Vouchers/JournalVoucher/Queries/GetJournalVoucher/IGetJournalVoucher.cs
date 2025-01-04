using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher
{
    public interface IGetJournalVoucher
    {
        ResultDto<JournalVoucherDto> Execute(long voucherMasters_Id);

    }
    public class GetJournalVoucher : IGetJournalVoucher
    {
        private readonly IDatabaseContext _context;

        public GetJournalVoucher(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<JournalVoucherDto> Execute(long voucherMasters_Id)
        {
            var data = this._context.sp_Voucher_GetById(voucherMasters_Id);

            return new ResultDto<JournalVoucherDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
