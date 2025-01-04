using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Account.Commands.DeleteAccountGroup
{
    public interface IDeleteAccountGroupService
    {
        ResultDto Execute(int accountGroup_Id);
    }
    public class DeleteAccountGroupService : IDeleteAccountGroupService
    {
        private readonly IDatabaseContext _context;


        public DeleteAccountGroupService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int accountGroup_Id)
        {
            try
            {

                bool error = false;
                if (_context.sp_AccountGroup_Delete(accountGroup_Id,out error) == 2)
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
