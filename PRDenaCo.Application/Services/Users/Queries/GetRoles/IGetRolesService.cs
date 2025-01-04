using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Users.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<RolesDto>> Execute();
    }
}
