using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.ExchangeRate.Commands.AddNewExchangeRate
{
    public interface IAddNewExchangeRateService
    {
        ResultDto Execute(ExchangeRateInfoById_Dto request);
    }

    public class AddNewExchangeRateService : IAddNewExchangeRateService
    {
        private readonly IDatabaseContext _context;


        public AddNewExchangeRateService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(ExchangeRateInfoById_Dto request)
        {
            try
            {
                if (_context.sp_ExchangeRate_Insert(request) == 1)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
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
