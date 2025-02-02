﻿using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Currencies.Commands.DeleteCurrency
{
   public interface IDeleteCurrencyService
    {
        ResultDto Execute(long currency_id);
    }

    public class DeleteCurrencyService : IDeleteCurrencyService
    {
        private readonly IDatabaseContext _context;


        public DeleteCurrencyService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(long currency_id)
        {
            try
            {

                bool error = false;
                if (_context.sp_CurrencyCompany_Delete(currency_id, out error) == 2)
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
