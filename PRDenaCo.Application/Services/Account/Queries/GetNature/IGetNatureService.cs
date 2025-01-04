using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountGroup;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Account.Queries.GetNature
{
    public interface IGetNatureService
    {
        ResultDto<int> Execute(int accountGroup_Id);
    }
    public class GetNatureService : IGetNatureService
    {
        private readonly IDatabaseContext _context;

        public GetNatureService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<int> Execute(int accountGroup_Id)
        {
            int result = _context.sp_AccountGroup_GetNature(accountGroup_Id);

            return new ResultDto<int>()
            {
                Data = result,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
