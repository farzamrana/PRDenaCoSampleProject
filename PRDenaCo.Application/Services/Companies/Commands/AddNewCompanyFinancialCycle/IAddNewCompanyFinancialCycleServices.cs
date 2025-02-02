﻿using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using PRDenaCo.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Companies.Commands.AddNewCompanyFinancialCycle
{
   public interface IAddNewCompanyFinancialCycleServices
    {
        ResultDto Execute(CompanyFinancialCycle_Dto request);
    }
    public class AddNewCompanyFinancialCycleServices : IAddNewCompanyFinancialCycleServices
    {
        private readonly IDatabaseContext _context;


        public AddNewCompanyFinancialCycleServices(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(CompanyFinancialCycle_Dto request)
        {
            try
            {
              /*  CompanyFinancialCycle companyFinancialCycle = new CompanyFinancialCycle()
                {
                    Company_Id = request.Company_Id,
                    FinancialCycle_FromDate = request.FinancialCycle_FromDate,
                    FinancialCycle_Id = request.FinancialCycle_Id,
                    FinancialCycle_isActive = request.FinancialCycle_isActive,
                    FinancialCycle_Title = request.FinancialCycle_Title,
                    FinancialCycle_ToDate = request.FinancialCycle_ToDate
                };*/

                if (_context.Sp_CompanyFinancialCycle_Insert(request) == -1)
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

   
}
