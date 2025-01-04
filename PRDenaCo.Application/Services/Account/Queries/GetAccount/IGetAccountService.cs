using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System.Collections.Generic;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountLedger;

namespace PRDenaCo.Application.Services.Account.Queries.GetAccount
{
    public interface IGetAccountService
    {
        ResultDto<AccountListDto> Execute(int Company_Id, int AccountGroup_Id, int CompanyUser_Id);
    }

    public class GetAccountService : IGetAccountService
    {
        private readonly IDatabaseContext _context;

        public GetAccountService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<AccountListDto> Execute(int Company_Id, int AccountGroup_Id, int CompanyUser_Id)
        {
            var accounts = this._context.sp_AccountLegder_Get(Company_Id, AccountGroup_Id, CompanyUser_Id);

            return new ResultDto<AccountListDto>()
            {
                Data = accounts,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class AccountListDto
    {
        public InFormAccess InFormAccess { get; set; }
        public List<AccountList> AccountList { get; set; }
        public AccountListDto()
        {
            InFormAccess = new InFormAccess();
            AccountList = new List<AccountList>();
        }
    }
    public class AccountList
    {
        public int Account_Node_Id { get; set; }
        public string Account_Node_Name { get; set; }
        public int Account_AccountGroup_Parent { get; set; }
        public int Account_Is_Group { get; set; }
    }
}