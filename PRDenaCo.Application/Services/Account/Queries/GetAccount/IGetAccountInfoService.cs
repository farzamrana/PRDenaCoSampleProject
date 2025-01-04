using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountLedger;

namespace PRDenaCo.Application.Services.Account.Queries.GetAccount
{
    public interface IGetAccountInfoService
    {
        ResultDto<AccountLedgerDto> Execute(int Company_Id, int AccountLeger_Id);
    }

    public class GetAccountInfoService : IGetAccountInfoService
    {
        private readonly IDatabaseContext _context;

        public GetAccountInfoService(IDatabaseContext context)
        {
            this._context = context;

        }

        public ResultDto<AccountLedgerDto> Execute(int Company_Id, int AccountLeger_Id)
        {
            var account_ledger = this._context.sp_AccountLegder_GetById(Company_Id, AccountLeger_Id);

            return new ResultDto<AccountLedgerDto>()
            {
                Data = account_ledger,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}