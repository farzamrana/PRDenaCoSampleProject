using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanyBranches;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Companies.Commands.AddNewCompanyBranch
{
    public interface IAddNewCompanyBranchServices
    {
        ResultDto Execute(CompanyBranch_Dto request);
    }
    public class AddNewCompanyBranchServices : IAddNewCompanyBranchServices
    {
        private readonly IDatabaseContext _context;


        public AddNewCompanyBranchServices(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(CompanyBranch_Dto request)
        {
            try
            {
               
                if (_context.sp_CompanyBranch_Insert(request) == -1)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };

                }
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
