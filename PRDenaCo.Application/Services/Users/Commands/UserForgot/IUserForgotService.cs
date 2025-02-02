﻿using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Users.Commands.UserForgot
{
   public interface IUserForgotService
    {
        ResultDto<ResultUserForgotDto> Execute(string username);
    }
    public class UserForgotService : IUserForgotService
    {
        private readonly IDatabaseContext _context;
        public UserForgotService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserForgotDto> Execute(string username)
        {
            byte errorType;
            ResultUserForgotDto resultUserForgotDto = _context.Sp_Users_Forget(username, out errorType);
            switch (errorType)
            {
                case 0:
                   
                    return new ResultDto<ResultUserForgotDto>()
                    {
                        Data = resultUserForgotDto ,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };

                case 1:
                    return new ResultDto<ResultUserForgotDto>()
                    {
                        Data = new ResultUserForgotDto()
                        {

                        },
                        IsSuccess = false,
                        Message = AppMessages.USER_NOT_FOUND,
                    };

                case 2:
                    return new ResultDto<ResultUserForgotDto>()
                    {
                        Data = new ResultUserForgotDto()
                        {

                        },
                        IsSuccess = false,
                        Message = AppMessages.USER_NOT_ACTIVATED,
                    };
               
            }

          

            return new ResultDto<ResultUserForgotDto>()
            {
                Data = new ResultUserForgotDto()
                {

                },
                IsSuccess = false,
                Message = AppMessages.UNKNOWN,
            };
        }
    }

    public class ResultUserForgotDto
    {
        public int Users_Id { get; set; }
        public int Accounts_Id { get; set; }
       
    }
}
