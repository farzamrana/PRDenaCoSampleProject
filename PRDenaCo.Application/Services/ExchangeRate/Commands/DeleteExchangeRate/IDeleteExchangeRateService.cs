using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.ExchangeRate.Commands.DeleteExchangeRate
{
    public interface IDeleteExchangeRateService
    {
        ResultDto Execute(long exchangerate_id);
    }

    public class DeleteExchangeRateService : IDeleteExchangeRateService
    {
        private readonly IDatabaseContext _context;


        public DeleteExchangeRateService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(long exchangerate_id)
        {
            try
            {

                bool error = false;
                if (_context.sp_ExchangeRate_Delete(exchangerate_id, out error) == 2)
                {
                    if (error == true)
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
                            Message = AppMessages.UNABLE_DELETE,
                        };

                    }
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
