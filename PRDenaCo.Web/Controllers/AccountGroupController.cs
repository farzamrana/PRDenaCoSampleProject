﻿using PRDenaCo.Application.Services.Account.Commands.AddNewAccountGroup;
using PRDenaCo.Application.Services.Account.Commands.DeleteAccountGroup;
using PRDenaCo.Application.Services.Account.Queries.GetAccess;
using PRDenaCo.Application.Services.Account.Queries.GetAccountGroup;
using PRDenaCo.Application.Services.Account.Queries.GetNature;
using PRDenaCo.Application.Services.Common.Queries.GetListItem;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using PRDenaCo.Web.Models;
using PRDenaCo.Web.Models.Account;
using PRDenaCo.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PRDenaCo.Web.Helper;

namespace PRDenaCo.Web.Controllers
{
    public class AccountGroupController : BaseController
    {
        
             private readonly IGetAccountGroupService _getAccountGroupService;
        private readonly IAddNewAccountGroupService _accountGroupService;
        private readonly IGetAccountGroupInfoService _getAccountGroupInfoService;
        
             private readonly IDeleteAccountGroupService _deleteAccountGroupService;
        private readonly IGetNatureService _getNatureService;
        private readonly IGetAccessService _getAccessService;
        private readonly IGetListItemService _getListItemService;


        public AccountGroupController(IGetAccountGroupService getAccountGroupService, IAddNewAccountGroupService accountGroupService, IGetAccountGroupInfoService getAccountGroupInfoService, IDeleteAccountGroupService deleteAccountGroupService, IGetNatureService getNatureService, IGetAccessService getAccessService, IGetListItemService getListItemService)
        {
            this._getAccountGroupService = getAccountGroupService;
            this._accountGroupService = accountGroupService;
            this._getAccountGroupInfoService = getAccountGroupInfoService;
            this._deleteAccountGroupService = deleteAccountGroupService;
            this._getNatureService = getNatureService;
            this._getAccessService = getAccessService;
            this._getListItemService = getListItemService;
        }
        public IActionResult Index(int id)
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            //ViewBag.Company_Id = id;
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<InFormAccess> result = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View();
        }
        public IActionResult ViewInfo(int id)
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.HasChild = false;
            ResultDto<RequestAccountGroupDto> result = _getAccountGroupInfoService.Execute(activeUser.Company_Id, (id < 0) ? 0 : id,0);
            if (result.IsSuccess)
            {
                ViewBag.Natures = new SelectList(result.Data.Natures, "Nature_Id", "Nature_Description");
                ViewBag.AccountGroups = new SelectList(result.Data.AccountGroups, "AccountGroup_Id", "AccountGroup_Name");
               

                return View(DtosToModels.AccountGroupToModel(result.Data.RequestAccountGroup));
            }
            else
                return Json(result);

        }
            [NoDirectAccess]
        
        public IActionResult AddOrEdit(int id/*JsTreeOperationData data*/)
        {
            //if id<0 New Mode
            //If id>0 Edit Mode
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.HasChild = false;
            ViewBag.IsRoot = false;
            if (id == 0)
                ViewBag.IsRoot = true;
            ResultDto<RequestAccountGroupDto> result = _getAccountGroupInfoService.Execute(activeUser.Company_Id,(id<0)?0:id, (id < 0)?id*-1:0);
            if (result.IsSuccess)
            {
                ViewBag.Natures = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.Natures, result.Data.Natures).Data);// new SelectList(result.Data.Natures, "Nature_Id", "Nature_Description");
                ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups, result.Data.AccountGroups).Data);// new SelectList(result.Data.AccountGroups, "AccountGroup_Id", "AccountGroup_Name");
                if (id < 0)
                {
                    result.Data.RequestAccountGroup.AccountGroup_Parent = id * -1;
                    //result.Data.RequestAccountGroup.Company_Id = activeUser.Company_Id;
                }
                else { 
                    ViewBag.HasChild = result.Data.HasChild;
                   
                }


                return View(DtosToModels.AccountGroupToModel(result.Data.RequestAccountGroup));
            }
            else
                return Json(result);
        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, AccountGroupModel request)
        {
            ViewBag.Natures = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.Natures).Data);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups).Data);

            if (ModelState.IsValid)
            {
                ActiveUser activeUser = CurrentUser.Get();

                request.Company_Id = activeUser.Company_Id;
               
                ResultDto result = _accountGroupService.Execute(Utilities.ModelsToDtos.AccountGroupToDto(request));
                if (result.IsSuccess)
                {
                    //ResultDto<InFormAccess> dto = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
                    //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
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


        public ActionResult GetNatureJson(int id)
        {
           ResultDto<int> result= _getNatureService.Execute(id);
            if (result.IsSuccess)
            {
                return Json(result.Data);
            }else
                return Json(result);

        }
        [HttpPost]
        public ActionResult GetTreeJson()
        {
            ActiveUser activeUser = CurrentUser.Get(); //SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
           
            ResultDto<InFormAccess> accessResult = this._getAccessService.Execute(activeUser.CompanyUsers_Id);

            var nodesList = new List<JsTreeNode>();
            ResultDto<AccountGroupListDto> result = this._getAccountGroupService.Execute(activeUser.Company_Id, activeUser.CompanyUsers_Id);

            foreach (AccountGroupList dto in result.Data.AccountGroupList)
            {
                if (dto.AccountGroup_Id == 0)
                {
                    var rootNode = new JsTreeNode()
                    {
                        id = dto.AccountGroup_Id.ToString(),
                        //text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='btn-group account-accounts' style='float:right;display:none'>  " +
                        text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='btn-group account-accounts' style='float:right;padding-right:10px'>  " +
                         (accessResult.Data.Insert_Row > 0 ? "<button type='button' class='btn  btn-success account-icons'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'new')\"><i class='fa fa-file'></i> </button>" : "") +
                         "</div>",
                        //text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='btn-group account-accounts' style='float:right;padding-right:15px'>  " +
                        //    (accessResult.Data.Insert_Row > 0 ? "<button type='button' class='btn btn-sm btn-primary new-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'view')\"><i class='ti-receipt' ></i></button>" : "") +
                        // (accessResult.Data.Insert_Row > 0 ? "<button type='button' class='btn btn-sm btn-success new-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'new')\"><i class='ti-file' ></i></button>" : "") +
                        //    (accessResult.Data.Edit_Row > 0 ? "<button type='button' class='btn btn-sm btn-warning edit-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'edit')\"><i class='ti-pencil'></i></button>" : "") +
                        // "</div>",
                        li_attr =new JsTreeNodeLiAttributes() {data= dto.Company_Id.ToString() }
                    };
                    nodesList.Add(rootNode);
                    PopulateTree(result.Data.AccountGroupList, rootNode, accessResult.Data);
                }

            }
     
          

            return Json(nodesList);
        }
        public void PopulateTree(List<AccountGroupList> dtos, JsTreeNode parentNode, InFormAccess inFormAccess)
        {
            foreach (AccountGroupList dto in dtos)
            {
                if (dto.AccountGroup_Parent == parentNode.id.ToInt())
                {
                    JsTreeNode node = new JsTreeNode()
                    {
                        id = dto.AccountGroup_Id.ToString(),
                        //text = "<span>"+ dto.AccountGroup_Name + "</span>" + "<div id=div_"+ dto.AccountGroup_Id.ToString() + " class='btn-group account-accounts' style='float:right;display:none'>  " +

                        //text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='btn-group account-accounts' style='float:right;padding-right:10px'>  " +
                        //(inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn btn-sm  new-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'view')\"><i class='ti-receipt' style='color:blue;font-weight:bold'></i></button>" : "") +
                        // (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn btn-sm   new-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'new')\"><i class='ti-file' style='color:green;font-weight:bold'></i></button>" : "") +
                        //    (inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn btn-sm  edit-account-ledger' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'edit')\"><i class='ti-pencil' style='color:orange;font-weight:bold'></i></button>" : "") +
                        //    (inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn  btn-sm  remove-account-ledger'  onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'delete')\"><i class='ti-trash' style='color:red;font-weight:bold'></i></button>" : "") +
                        //    "</div>",
                        //text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                        //(inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn  btn-outline-info' style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>" : "") +
                        // (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn  btn-outline-success'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'new')\"><i class='fa fa-file'></i> </button>" : "") +
                        //    (inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn  btn-outline-warning'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i> </button>" : "") +
                        //    (inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn  btn btn-outline-danger'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'delete')\"><i class='fa fa-trash'></i></button>" : "") +
                        //    "</div>",
                        text = "<span>" + dto.AccountGroup_Name + "</span>" + "<div id=div_" + dto.AccountGroup_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                         "<button type='button' class='btn  btn-info' style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>"  +
                         (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn  btn-success'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'new')\"><i class='fa fa-file'></i> </button>" : "") +
                            (dto.Company_Id>0 && inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn  btn-warning'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i> </button>" : "") +
                            (dto.Company_Id > 0 && inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn  btn btn-danger'  style='margin-right:3px' onclick=\"showPopup(" + dto.AccountGroup_Id.ToString() + ",'delete')\"><i class='fa fa-trash'></i></button>" : "") +
                            "</div>",
                        li_attr = new JsTreeNodeLiAttributes() { data = dto.Company_Id.ToString() }
                      //  icon = (dto.MenuOptions_Title == "Edit") ? Url.Content("~/media/image/bookmark_book_open.png") : (dto.MenuOptions_Title == "Add") ? Url.Content("~/media/image/add.png") : (dto.MenuOptions_Title == "Delete") ? Url.Content("~/media/image/delete.png") : "",
                    };
                    parentNode.children.Add(node);
                    PopulateTree(dtos, node, inFormAccess);

                }
            }

        }

        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ConfirmMessage = AppMessages.DELETE_CONFIRM;
            ViewBag.accountGroup_Id = id;
            return View();
        }

        [HttpPost, ActionName("Delete")]
      
        public IActionResult DeleteConfirmed(int id)
        {
          

          ResultDto result=  _deleteAccountGroupService.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                ResultDto<InFormAccess> dto = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
            }else
                return Json(result);
        }


        [HttpPost, ActionName("Close")]
        public IActionResult CloseDialog()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<InFormAccess> dto = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
        }
    }
}
