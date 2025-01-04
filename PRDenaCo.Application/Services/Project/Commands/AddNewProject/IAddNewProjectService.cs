using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.CostCenter.Commands.IAddNewCostCenterService;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Project.Commands.AddNewProject
{
    public interface IAddNewProjectService
    {
        ResultDto Execute(int Company_Id, ProjectDto project );
    }
    public class AddNewProjectService : IAddNewProjectService
    {
        private readonly IDatabaseContext _context;

        public AddNewProjectService(IDatabaseContext context)
        {
            this._context = context;
        }

        public ResultDto Execute(int Company_Id, ProjectDto project )
        {
            try
            {
                if (this._context.sp_Project_Insert(Company_Id, project ) == 2)
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

    public class ProjectDto
    {
        public int Projects_Id { get; set; }
        public string Projects_Number { get; set; }
        public string Projects_Name { get; set; }
        public string Projects_Description { get; set; }
        public DateTime Projects_StartDate { get; set; }
        public DateTime Projects_EndDate { get; set; }
        public int CostCenter_Id { get; set; }
        public byte Projects_Status { get; set; }
        public List<CostCenterDto> CostCenterList { get; set; }

        public List<ProjectStatusDto> ProjectStatusList { get; set; }

        public ProjectDto()
        {
            this.Projects_Id = 0;
            this.Projects_Number = "";
            this.Projects_Name = "";
            this.Projects_Description = "";
            this.CostCenter_Id = 0;
            this.Projects_Status = 0;
            this.CostCenterList = new List<CostCenterDto>();
            this.ProjectStatusList = new List<ProjectStatusDto>();
        }
    }

    public class ProjectStatusDto
    {
        public byte Status_Id { get; set; }
        public string Status_Description { get; set; }
    }
}
