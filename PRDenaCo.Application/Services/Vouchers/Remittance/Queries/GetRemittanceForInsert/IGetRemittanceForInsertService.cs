using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert
{
   public interface IGetRemittanceForInsertService
    {
        ResultDto<List<RemittanceCurrenciesDto>> Execute(int Company_Id, long Currency_Id);

    }
    public class GetRemittanceForInsertService : IGetRemittanceForInsertService
    {
        private readonly IDatabaseContext _context;

        public GetRemittanceForInsertService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<List<RemittanceCurrenciesDto>> Execute(int Company_Id, long Currency_Id)
        {
            var data = this._context.sp_Remittance_GetForInsert(Company_Id, Currency_Id);

            return new ResultDto<List<RemittanceCurrenciesDto>>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
 


    public class RemittanceCurrenciesDto
    {
        public int RemmitenceBatch_Id { set; get; }
        public decimal RemmitenceBatch_Rate { set; get; }
        public decimal RemmitenceBatch_Remaining { set; get; }
        public decimal RemittanceSell_Amount { set; get; }
        public int RemittanceSell_Id { set; get; }
        public int RemittanceSell_AmountInserted { set; get; }
    }
    public class RemittanceCurrencies_Dto
    {
        public float Rate { set; get; }
        
        public List< RemittanceCurrenciesDto> RemittanceCurrenciesDto { set; get; }
    }
}
