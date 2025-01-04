using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.CostCenter.Commands.IAddNewCostCenterService;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;

namespace PRDenaCo.Application.Services.CostCenter.Queries.GetCostCenters
{
    public interface IGetCostCenterService
    {
        ResultDto<List<CostCenterListDto>> Execute(int Company_Id);
    }

    public class GetCostCenterService : IGetCostCenterService
    {
        private readonly IDatabaseContext _context;

        public GetCostCenterService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<List<CostCenterListDto>> Execute(int Company_Id)
        {
            List<CostCenterListDto> costcenters = this._context.sp_CostCenter_List(Company_Id);

            return new ResultDto<List<CostCenterListDto>>()
            {
                Data = costcenters,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class CostCenterListDto
    {
        public int CostCenter_Id { get; set; }
       
        public string CostCenter_Name { get; set; }
        
        public string Status_Description { get; set; }
        public bool CostCenter_Used { get; set; }
    }
}
