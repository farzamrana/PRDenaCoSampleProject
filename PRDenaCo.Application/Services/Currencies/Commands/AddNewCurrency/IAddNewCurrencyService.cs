using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Currencies.Queries.GetCurrencies;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Currencies.Commands.AddNewCurrency
{
   public interface IAddNewCurrencyService
    {
        ResultDto Execute(Currency_Dto request);
    }

    public class AddNewCurrencyService : IAddNewCurrencyService
    {
        private readonly IDatabaseContext _context;


        public AddNewCurrencyService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(Currency_Dto request)
        {
            try
            {
                if (_context.sp_CurrencyCompany_Insert(request) == 1)
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
