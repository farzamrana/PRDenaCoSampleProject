using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Account.Commands.DeleteAccountLedger
{
   public interface IDeleteAccountLedgerService
    {
        ResultDto Execute(int accountLedger_Id);
    }
    public class DeleteAccountLedgerService : IDeleteAccountLedgerService
    {
        private readonly IDatabaseContext _context;


        public DeleteAccountLedgerService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int accountLedger_Id)
        {
            try
            {

                bool error = false;
                if (_context.sp_AccountLedger_Delete(accountLedger_Id, out error) == 2)
                {
                    if (error == false)
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
                            IsSuccess = true,
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
