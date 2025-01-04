using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountGroup;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Account.Queries.GetAccountGroup
{
    public interface IGetAccountGroupService
    {
        ResultDto<AccountGroupListDto> Execute(int company_Id, int CompanyUsers_Id);
    }
    public class GetAccountGroupService : IGetAccountGroupService
    {
        private readonly IDatabaseContext _context;

        public GetAccountGroupService(IDatabaseContext context)
        {
            _context = context;
        }
     
        public ResultDto<AccountGroupListDto> Execute(int company_Id, int CompanyUsers_Id)
        {
            var accountGroups = _context.sp_AccountGroup_Get(company_Id, CompanyUsers_Id);

            return new ResultDto<AccountGroupListDto>()
            {
                Data = accountGroups,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class AccountGroupListDto
    {
        public InFormAccess InFormAccess { get; set; }
        public List<AccountGroupList> AccountGroupList { get; set; }
        public AccountGroupListDto()
        {
            InFormAccess = new InFormAccess();
            AccountGroupList = new List<AccountGroupList>();
        }
    }
    public class AccountGroupList
    {
        public int AccountGroup_Id { get; set; }
        public int AccountGroup_Parent { get; set; }
        public string AccountGroup_Name { get; set; }
        public int Company_Id { get; set; }
    }
}
