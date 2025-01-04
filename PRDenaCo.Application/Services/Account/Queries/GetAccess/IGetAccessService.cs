using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Account.Queries.GetAccess
{
    public interface IGetAccessService
    {
        ResultDto<InFormAccess> Execute(int CompanyUsers_Id);
    }
    public class GetAccessService : IGetAccessService
    {
        private readonly IDatabaseContext _context;

        public GetAccessService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<InFormAccess> Execute(int CompanyUsers_Id)
        {
            InFormAccess result = _context.sp_AccountGroup_GetAccess(CompanyUsers_Id);

            return new ResultDto<InFormAccess>()
            {
                Data = result,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
