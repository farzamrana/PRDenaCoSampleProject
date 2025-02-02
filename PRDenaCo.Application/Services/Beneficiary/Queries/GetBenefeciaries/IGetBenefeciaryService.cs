﻿using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Beneficiary.Queries.GetBenefeciaries
{
    public interface IGetBenefeciaryService
    {
        ResultDto<List<BenefeciaryList_Dto>> Execute(int Company_Id);
    }

    public class GetBenefeciaryService : IGetBenefeciaryService
    {
        private readonly IDatabaseContext _context;
        public GetBenefeciaryService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<BenefeciaryList_Dto>> Execute(int Company_Id)
        {
            List<BenefeciaryList_Dto> benefeciaries = _context.sp_Beneficiary_List(Company_Id);
            return new ResultDto<List<BenefeciaryList_Dto>>()
            {
                Data = benefeciaries,
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class BenefeciaryList_Dto
    {
        public int Beneficiary_Id { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Beneficiary_Mobile { get; set; }
        public string Beneficiary_Passport { get; set; }
        public string Beneficiary_IdNumber { get; set; }
    }
}
