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
    public interface IGetProjectService 
    {
        ResultDto<List<ProjectListDto>> Execute(int company_id);
    }
    public class GetProjectService : IGetProjectService
    {
        private readonly IDatabaseContext _context;

        public GetProjectService(IDatabaseContext context)
        {
            this._context = context;

        }
        public ResultDto<List<ProjectListDto>> Execute(int company_id)
        {
            List<ProjectListDto> projects = this._context.sp_Project_List(company_id);

            return new ResultDto<List<ProjectListDto>>()
            {
                Data = projects,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class ProjectListDto
    {
        public int Projects_Id { get; set; }
        public string Projects_Number { get; set; }
        public string Projects_Name { get; set; }
        public string Status_Description { get; set; }
        public bool Projects_Used { get; set; }

    }
}
