using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.SuffixPrefix.Commands.AddNewSuffixPrefix
{
    public interface IAddNewSuffixPrefixService
    {
        ResultDto Execute(SuffixPrefix_Dto request);
    }
    public class AddNewSuffixPrefixService : IAddNewSuffixPrefixService
    {
        private readonly IDatabaseContext _context;


        public AddNewSuffixPrefixService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(SuffixPrefix_Dto request)
        {
            try
            {
               

                if (_context.sp_SuffixPrefix_Insert(request) == -1)
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
