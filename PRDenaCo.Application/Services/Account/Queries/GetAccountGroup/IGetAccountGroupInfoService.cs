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
    public interface IGetAccountGroupInfoService
    {
        public ResultDto<RequestAccountGroupDto> Execute(int Company_Id,int AccountGroup_Id, int AccountGroup_Parent);
    }
    public class GetAccountGroupInfoService : IGetAccountGroupInfoService
    {
        private readonly IDatabaseContext _context;

        public GetAccountGroupInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<RequestAccountGroupDto> Execute(int Company_Id, int AccountGroup_Id,int AccountGroup_Parent)
        {
            try
            {
                RequestAccountGroupDto dto = _context.sp_AccountGroup_GetById(AccountGroup_Id, Company_Id, AccountGroup_Parent);
                if (dto != null)
                {
                    return new ResultDto<RequestAccountGroupDto>
                    {
                        Data = dto,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<RequestAccountGroupDto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<RequestAccountGroupDto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
