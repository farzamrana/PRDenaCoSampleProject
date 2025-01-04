using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix
{
   public interface ILoadSuffixPrefixService
    {
        ResultDto<List< VoucherTypeDto>> Execute();

    }
    public class LoadSuffixPrefixService : ILoadSuffixPrefixService
    {
        private readonly IDatabaseContext _context;

        public LoadSuffixPrefixService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<List<VoucherTypeDto>> Execute()
        {
            var data = this._context.sp_SuffixPrefix_Load();

            return new ResultDto<List<VoucherTypeDto>>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class VoucherTypeDto
    {
        public int voucherType_Id { get; set; }
        public string voucherType_Name { get; set; }
    }
}
