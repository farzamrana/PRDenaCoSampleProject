using PRDenaCo.Application.Interfaces.Contexts;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRDenaCo.Application.Services.Beneficiary.Commands.AddNewBenefeciary
{
   public interface IAddNewBenefeciaryService
    {
        ResultDto Execute(Benefeciary_Dto request);
    }

    public class AddNewBenefeciaryService : IAddNewBenefeciaryService
    {
        private readonly IDatabaseContext _context;


        public AddNewBenefeciaryService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(Benefeciary_Dto request)
        {
            try
            {
                if (_context.sp_Beneficiary_Insert(request) == 1)
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

    public class Benefeciary_Dto
    {
        public int Beneficiary_Id { get; set; }
        public int Company_Id { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Beneficiary_Mobile { get; set; }
        public string Beneficiary_Passport { get; set; }
        public string Beneficiary_RefNo { get; set; }
        public string Beneficiary_Remark { get; set; }
        public string Beneficiary_IdNumber { get; set; }
    }
}
