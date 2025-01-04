using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Users.Queries.GetRoles;
using PRDenaCo.Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Bugeto_Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDatabaseContext _context;

        public GetRolesService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RolesDto>> Execute()
        {
            var roles = _context.Role_GetAll().ToList().Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
