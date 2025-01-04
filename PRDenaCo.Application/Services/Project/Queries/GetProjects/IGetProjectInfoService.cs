using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Project.Commands.AddNewProject;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Project.Queries.GetProjects
{
    public interface IGetProjectInfoService
    {
        ResultDto<ProjectDto> Execute(int Company_Id, int Project_Id);
    }
    public class GetProjectInfoService : IGetProjectInfoService
    {
        private readonly IDatabaseContext _context;

        public GetProjectInfoService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<ProjectDto> Execute(int Company_Id, int Project_Id)
        {
            var project = this._context.sp_Projects_GetById(Company_Id, Project_Id);

            return new ResultDto<ProjectDto>()
            {
                Data = project,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
