using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Common.Commands.UserProfile
{
    public interface IUserProfileService
    {
        ResultDto Execute(UserProfileDto request);
    }
    public class UserProfileService : IUserProfileService
    {
        private readonly IDatabaseContext _context;


        public UserProfileService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(UserProfileDto request)
        {
            try
            {

                if (_context.sp_UsersProfile_Insert(request) == -1)
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
    public class UserProfileDto
    {
        public int Users_Id { set; get; }
        public bool UsersProfile_isFullscreen { set; get; }
        public bool UsersProfile_isLight { set; get; }
        public byte UsersProfile_TemplateKind { set; get; }
        public byte InsertKind { set; get; }

    }
}
