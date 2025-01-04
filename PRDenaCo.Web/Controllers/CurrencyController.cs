using PRDenaCo.Application.Services.Currencies.Commands.AddNewCurrency;
using PRDenaCo.Application.Services.Currencies.Queries.GetCurrencies;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
 
using PRDenaCo.Web.Models.Currency;
using PRDenaCo.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PRDenaCo.Web.Helper;

namespace PRDenaCo.Web.Controllers
{
    public class CurrencyController : BaseController
    {

        private readonly IGetCurrencyService _getCurrencyService;
        private readonly IAddNewCurrencyService _addnewCurrencyService;
        private readonly IGetCurrencyInfoService _getCurrencyInfoService;
        
        public CurrencyController (IGetCurrencyService getCurrencyService, IAddNewCurrencyService addnewCurrencyService, IGetCurrencyInfoService getCurrencyInfoService)
        {
            this._getCurrencyService = getCurrencyService;
            this._addnewCurrencyService = addnewCurrencyService;
            this._getCurrencyInfoService = getCurrencyInfoService;
        }
        public IActionResult Index()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ResultDto<List<CurrencyList_Dto>> result = _getCurrencyService.Execute(activeUser.Company_Id);
            return View(DtosToModels.CurrencyToModel(result.Data));
        }

        [NoDirectAccess]
        public IActionResult AddOrEdit(long id )
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            if (id > 0)
            {
                ResultDto<Currency_Dto> result = _getCurrencyInfoService.Execute(id);
                if (result.IsSuccess)
                {
                    return View(DtosToModels.CurrencyInfoToModel(result.Data));
                }
                else
                    return Json(result);
            }
            else
            {
                ActiveUser activeUser = CurrentUser.Get();
                return View(new CurrencyInfoModel() { Company_Id = activeUser.Company_Id });
            }
        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(CurrencyInfoModel request)
        {
            if (ModelState.IsValid)
            {
                ResultDto result = _addnewCurrencyService.Execute(Utilities.ModelsToDtos.CurrencyInfoToDto(request));
                if (result.IsSuccess)
                {
                    ActiveUser activeUser = CurrentUser.Get();
                    // return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CurrencyToModel(_getCurrencyService.Execute(activeUser.Company_Id).Data)) });
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Success) };
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

                }
                else
                {
                    //return Json(result);
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Warning) };
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

                }
            }
            request.OprMessage = new Models.MessageViewModel() { Message = AppMessages.REQUIRED, Color = AppMessages.GetMessageColor(MessageType.Error) };
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

        }

        [HttpPost, ActionName("Close")]
        public IActionResult CloseDialog()
        {
            ActiveUser activeUser = CurrentUser.Get();
            int Accounts_Id = activeUser.Accounts_Id;
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CurrencyToModel(_getCurrencyService.Execute(activeUser.Company_Id).Data)) });
        }
    }
}
