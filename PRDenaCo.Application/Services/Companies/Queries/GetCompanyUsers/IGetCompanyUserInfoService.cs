using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Companies.Queries.GetCompanyUsers
{
   public interface IGetCompanyUserInfoService
    {
        public ResultDto<CompanyUserDto> Execute(int companyUsers_Id);
    }
    public class GetCompanyUserInfoService : IGetCompanyUserInfoService
    {
        private readonly IDatabaseContext _context;


        public GetCompanyUserInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<CompanyUserDto> Execute(int companyUsers_Id)
        {
            try
            {
                CompanyUserDto companyUserDto = _context.Sp_CompanyUser_Get(companyUsers_Id);
                if (companyUserDto != null)
                {
                    return new ResultDto<CompanyUserDto>
                    {
                        Data = companyUserDto ,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<CompanyUserDto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<CompanyUserDto>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }
    }
}
