using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Common.Queries.GetCurrentDate
{
  public   interface IGetCurrentDateService
    {
        ResultDto<DateTime> Execute(int Company_Id);
    }
    public class GetCurrentDateService : IGetCurrentDateService
    {
        private readonly IDatabaseContext _context;
        public GetCurrentDateService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<DateTime> Execute(int Company_Id)
        {
            try
            {
                DateTime currDate;
                int result = _context.sp_Company_GetDate(Company_Id,out currDate);
                if (result != -2)
                {
                    return new ResultDto<DateTime>
                    {
                        Data = currDate,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<DateTime>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<DateTime>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

}
