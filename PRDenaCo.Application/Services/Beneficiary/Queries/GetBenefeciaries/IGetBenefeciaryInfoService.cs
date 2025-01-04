using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Beneficiary.Queries.GetBenefeciaries
{
    public interface IGetBenefeciaryInfoService
    {
        ResultDto<Benefeciary_Dto> Execute(int beneficiary_id);
    }
    public class GetBenefeciaryInfoService : IGetBenefeciaryInfoService
    {
        private readonly IDatabaseContext _context;


        public GetBenefeciaryInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<Benefeciary_Dto> Execute(int beneficiary_id)
        {
            try
            {
                Benefeciary_Dto benefeciary= _context.sp_Beneficiary_GetById(beneficiary_id);
                if (benefeciary != null)
                {
                    return new ResultDto<Benefeciary_Dto>
                    {
                        Data = benefeciary,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<Benefeciary_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<Benefeciary_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
