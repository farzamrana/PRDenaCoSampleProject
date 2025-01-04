using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRDenaCo.Application.Services.Products.Queries.GetCompanies;
using PRDenaCo.Application.Services.Companies.Commands.AddNewCompany;
using PRDenaCo.Application.Services.Companies.Commands.AddNewCompanyFinancialCycle;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanies;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanyUsers;
using PRDenaCo.Application.Services.Companies.Commands.AddNewCompanyUser;
using PRDenaCo.Application.Services.Users.Commands.UserChange;
using PRDenaCo.Application.Services.Companies.Queries.GetCompanyBranches;
using PRDenaCo.Application.Services.Companies.Commands.AddNewCompanyBranch;
using PRDenaCo.Application.Services.Companies.Commands.DeleteCompanyUser;

namespace PRDenaCo.Application.Interfaces.FacadPatterns
{
    public interface ICompanyFacad
    {
        /// <summary>
        /// سرویس اضافه کردن دوره مالی شرکت
          /// </summary>
        IAddNewCompanyFinancialCycleServices AddNewCompanyFinancialCycleServices { get; }

        /// <summary>
        /// سرویس اضافه کردن شعبه شرکت
        /// </summary>
        IAddNewCompanyBranchServices AddNewCompanyBranchServices { get; }

        /// <summary>
        /// سرویس دریافت دوره های مالی شرکت
        /// </summary>
        IGetCompanyFinancialCycleServices GetCompanyFinancialCycleServices { get; }

        /// <summary>
        /// سرویس دریافت شعبه های شرکت
        /// </summary>
        IGetCompanyBranchServices GetCompanyBranchServices { get; }
        /// <summary>
        /// سرویس اضافه کردن کمپانی
        /// </summary>
        IAddNewCompanyService AddNewCompanyService { get; }
        IGetCompanyService GetCompanyService { get; }
        /// <summary>
        /// دریافت اطلاعات شرکت
        /// </summary>
        IGetCompanyInfoService GetCompanyInfoService { get; }
        /// <summary>
        /// دریافت اطلاعات دوره مالی
        /// </summary>
        IGetCompanyFinancialCycleInfoService GetCompanyFinancialCycleInfoService { get; }
        /// <summary>
        /// دریافت اطلاعات شعبه
        /// </summary>
        IGetCompanyBranchInfoService GetCompanyBranchInfoService { get; }

        /// <summary>
        /// دریافت اطلاعات کاربران شرکت
        /// </summary>
        public IGetCompanyUsersServices GetCompanyUsersServices { get; }
        /// <summary>
        /// سرویس ثبت کاربر شرکت
        /// </summary>
        public IAddNewCompanyUserServices AddNewCompanyUserServices { get; }

        /// <summary>
        /// دریافت اطلاعات کاربر شرکت
        /// </summary>
        IGetCompanyUserInfoService GetCompanyUserInfoService { get; }

        /// <summary>
        /// حذف کاربر شرکت
        /// </summary>
        public IDeleteCompanyUserService DeleteCompanyUserService { get; }


    }
}
