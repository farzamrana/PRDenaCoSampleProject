using PRDenaCo.Application.Services.Account.Commands.AddNewAccountLedger;
using PRDenaCo.Application.Services.Account.Commands.DeleteAccountLedger;
using PRDenaCo.Application.Services.Account.Queries.GetAccess;
using PRDenaCo.Application.Services.Account.Queries.GetAccount;
using PRDenaCo.Application.Services.Common.Queries.GetListItem;
using PRDenaCo.Common;
using PRDenaCo.Common.Dtos;
using PRDenaCo.Web.Models;
using PRDenaCo.Web.Models.Account;
using PRDenaCo.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PRDenaCo.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IGetAccessService _getAccessService;
        private readonly IGetAccountService _getAccountService;
        private readonly IGetAccountInfoService _getAccountInfoService;
        private readonly IAddNewAccountLedgerService _addNewAccountLedgerService;
        private readonly IGetListItemService _getListItemService;
        
        private readonly IDeleteAccountLedgerService _deleteAccountLedgerService;

        public AccountController(IGetAccountService getAccountService, IGetAccessService getAccessService,
            IGetAccountInfoService getAccountInfoService, IAddNewAccountLedgerService addNewAccountLedgerService, 
            IGetListItemService getListItemService, IDeleteAccountLedgerService deleteAccountLedgerService)
        {
            this._getAccountService = getAccountService;
            this._getAccessService = getAccessService;
            this._getAccountInfoService = getAccountInfoService;
            this._addNewAccountLedgerService = addNewAccountLedgerService;
            this._getListItemService = getListItemService;
            this._deleteAccountLedgerService = deleteAccountLedgerService;
        }
        //public IActionResult Index()
        //{
        //    return this.loadAccounts(0);
        //}
        //public IActionResult Customer()
        //{
        //    return this.loadAccounts(26);
        //}
        //public IActionResult Supplier()
        //{
        //    return this.loadAccounts(22);
        //}
        //[HttpGet("Account/Index/{accountGroupId}")]
        public IActionResult Index(int accountGroupId)
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<InFormAccess> result = this._getAccessService.Execute(activeUser.CompanyUsers_Id);

            if (result.IsSuccess)
            {
                ViewBag.AccountGroupId = accountGroupId;
                return View(result.Data);
            }

            return View();
        }
        //[HttpGet("Account/Index/AddOrEdit/{id}")]
        public IActionResult AddOrEdit(int id/*, int accountGroup_Id*/)
        {
            //if id<0 New Mode
            //If id>0 Edit Mode
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            ResultDto<AccountLedgerDto> accountList = this._getAccountInfoService.Execute(activeUser.Company_Id, (id < 0) ? 0 : id);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups, accountList.Data.AcountGroupList).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
            ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList, accountList.Data.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
            ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, accountList.Data.CurrencyCompanyList).Data);

            if (accountList.IsSuccess)
            {
                if (id < 0)
                    accountList.Data.AccountGroup_Id = id*-1 ;
                return View(DtosToModels.AccountToModel(accountList.Data));
            }

            return View();
        }


        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, AccountModel request)
        {
            if (ModelState.IsValid)
            {
                ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");

                request.Company_Id = activeUser.Company_Id;

                ResultDto result = this._addNewAccountLedgerService.Execute(Utilities.ModelsToDtos.AccountToDto(request));

                ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
                ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
                ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);

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

        public IActionResult ViewInfo(int id)
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<AccountLedgerDto> accountList = this._getAccountInfoService.Execute(activeUser.Company_Id, (id < 0) ? 0 : id);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountGroups, accountList.Data.AcountGroupList).Data); //new SelectList(accountList.Data.AcountGroupList, "AccountGroup_Id", "AccountGroup_Name");
            ViewBag.TermsAndConditions = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.TermsAndConditionList, accountList.Data.TermsAndConditionList).Data);// new SelectList(accountList.Data.TermsAndConditionList, "TermsAndCondition_Id", "TermsAndCondition_Name");
            ViewBag.CurrencyCompanies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, accountList.Data.CurrencyCompanyList).Data);

            if (accountList.IsSuccess)
            {
                if (id < 0)
                    accountList.Data.AccountGroup_Id = id * -1;
                return View(DtosToModels.AccountToModel(accountList.Data));
            }

            return View();

        }
        //[HttpPost]
        //public IActionResult Save(AccountModel accountModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");

        //        ResultDto result = this._addNewAccountLedgerService.Execute(activeUser.Company_Id, new AccountLedgerDto()
        //        {
        //            Ledger_Id = accountModel.Ledger_Id,
        //            Ledger_Name = accountModel.Ledger_Name,
        //            AccountGroup_Id = accountModel.AccountGroup_Id,
        //            Ledger_Code = accountModel.Ledger_Code,
        //            Currency_Id = accountModel.Currency_Id,
        //            Ledger_BillByBill = accountModel.Ledger_BillByBill,
        //            Ledger_Status = accountModel.Ledger_Status,
        //            //Bank
        //            LedgerBank_BankName = accountModel.LedgerBank_BankName,
        //            LedgerBank_BranchName = accountModel.LedgerBank_BranchName,
        //            LedgerBank_BranchCode = accountModel.LedgerBank_BranchCode,
        //            LedgerBank_AccountNumber = accountModel.LedgerBank_AccountNumber,
        //            LedgerBank_AccountName = accountModel.LedgerBank_AccountName,
        //            LedgerBank_IBAN = accountModel.LedgerBank_IBAN,
        //            LedgerBank_Swift = accountModel.LedgerBank_Swift,
        //            LedgerBank_HeaderNote = accountModel.LedgerBank_HeaderNote,
        //            LedgerBank_FooterNote = accountModel.LedgerBank_FooterNote,
        //            //Bank
        //            //Details
        //            LedgerDetails_CreditLimit = accountModel.LedgerDetails_CreditLimit,
        //            LedgerDetails_CreditPeriod = accountModel.LedgerDetails_CreditPeriod,
        //            LedgerDetails_MailingName = accountModel.LedgerDetails_MailingName,
        //            LedgerDetails_Branch = accountModel.LedgerDetails_Branch,
        //            LedgerDetails_Email = accountModel.LedgerDetails_Email,
        //            LedgerDetails_Address = accountModel.LedgerDetails_Address,
        //            LedgerDetails_ContactPerson = accountModel.LedgerDetails_ContactPerson,
        //            LedgerDetails_Mobile1 = accountModel.LedgerDetails_Mobile1,
        //            LedgerDetails_Mobile2 = accountModel.LedgerDetails_Mobile2,
        //            LedgerDetails_Phone = accountModel.LedgerDetails_Phone,
        //            LedgerDetails_Fax = accountModel.LedgerDetails_Fax,
        //            LedgerDetails_Narration = accountModel.LedgerDetails_Narration,
        //            LedgerDetails_BankIBAN = accountModel.LedgerDetails_BankIBAN,
        //            LedgerDetails_BankBranchName = accountModel.LedgerDetails_BankBranchName,
        //            LedgerDetails_BankBranchCode = accountModel.LedgerDetails_BankBranchCode,
        //            LedgerDetails_BankSwiftCode = accountModel.LedgerDetails_BankSwiftCode,
        //            LedgerDetails_BankAccountNumber = accountModel.LedgerDetails_BankAccountNumber,
        //            LedgerDetails_BankNameOnCheque = accountModel.LedgerDetails_BankNameOnCheque,
        //            LedgerDetails_ShipTo = accountModel.LedgerDetails_ShipTo,
        //            TermsAndCondition_Id = accountModel.TermsAndCondition_Id,
        //            LedgerDetails_CST = accountModel.LedgerDetails_CST,
        //            LedgerDetails_TIN = accountModel.LedgerDetails_TIN,
        //            LedgerDetails_VAT = accountModel.LedgerDetails_VAT,
        //            LedgerDetails_PAN = accountModel.LedgerDetails_PAN,
        //            //Details
        //        });

        //        if (result.IsSuccess)
        //        {
        //            return Json(new
        //            {
        //                isValid = true,
        //                message = "Account saved successfully!",
        //            });
        //        }
        //    }

        //    return Json(new
        //    {
        //        isValid = false,
        //        message = "Account saved failed! Please check fields and retry",
        //    });
        //}
        //private IActionResult loadAccounts(int accountGroupId = 0)
        //{
        //    ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
        //    ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
        //    ResultDto<InFormAccess> result = this._getAccessService.Execute(activeUser.CompanyUsers_Id);

        //    if (result.IsSuccess)
        //    {
        //        ViewData["AccountGroupId"] = accountGroupId;
        //        return View(result.Data);
        //    }

        //    return View();
        //}
        [HttpPost]
        public ActionResult GetTreeJson(string id, string accountGroupId)
        {
            int account_group_id = 0;

            try
            {
                account_group_id = int.Parse(accountGroupId);
            }
            catch (Exception ex)
            {
                account_group_id = 0;
            }

            ActiveUser activeUser = CurrentUser.Get();
            var nodesList = new List<JsTreeNode>();
            ResultDto<AccountListDto> accountList = this._getAccountService.Execute(activeUser.Company_Id, account_group_id, activeUser.CompanyUsers_Id);
            AccountListDto accountListData = accountList.Data;
            InFormAccess inFormAccess = new InFormAccess();

            List<AccountList> accounts = new List<AccountList>();

            if (accountListData != null && accountListData.AccountList.Count > 0)
            {
                foreach (AccountList account in accountListData.AccountList)
                {
                    accounts.Add(account);
                }

                inFormAccess.Insert_Row = accountListData.InFormAccess.Insert_Row;
                inFormAccess.Edit_Row = accountListData.InFormAccess.Edit_Row;
                inFormAccess.Delete_Row = accountListData.InFormAccess.Delete_Row;
            }

            if (accounts != null && accounts.Count > 0)
            {
                accounts.Sort((a, b) => a.Account_AccountGroup_Parent.CompareTo(a.Account_AccountGroup_Parent));

                int firstParentLevel = accounts[0].Account_AccountGroup_Parent;

                foreach (AccountList account in accounts)
                {
                    if (firstParentLevel == account.Account_AccountGroup_Parent)
                    {

                        //string jstree_actions = "<div class='btn-group account-accounts'>" +
                        //    (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn btn-success btn-sm add-account-ledger'><i class='ti-plus'></i></button>" : "") +
                        //    "</div>";

                        //string title = account.Account_Node_Name;

                        //if (account.Account_Is_Group == 1)
                        //{
                        //    title = "<span style='font-size: 0.8rem; font-weight: bold'>" + title + "</span>";
                        //}

                        //if (account.Account_Is_Group != 1)
                        //{
                        //    jstree_actions = "<div class='btn-group account-accounts'>" +
                        //        (inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn btn-warning btn-sm edit-account-ledger'><i class='ti-pencil'></i></button>" : "") +
                        //        (inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn btn-danger btn-sm remove-account-ledger'><i class='ti-trash'></i></button>" : "") +
                        //        "</div>";
                        //}

                        var rootNode = new JsTreeNode()
                        {
                            id = account.Account_Node_Id.ToString(),
                            // text = "<div>" + title + jstree_actions + "</div>",
                            text = "<span " + ((account.Account_Is_Group == 1) ? " style = 'font-size: 0.8rem; font-weight: bold' " : "") + ">" + account.Account_Node_Name + "</span>" + "<div id=div_" + account.Account_Node_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                             "<button type='button'  class='btn  btn-info' style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>" +
                         (inFormAccess.Insert_Row > 0 ? "<button type='button'class='btn  btn-success' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'new')\"><i class='fa fa-file'></i></button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Edit_Row > 0 ? "<button type='button'  class='btn  btn-warning'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i></button>" : "") +
                         "</div>",
                            state = new JsTreeNodeState()
                            {
                                selected = true,
                            },
                            li_attr = new JsTreeNodeLiAttributes()
                            {
                                data = account.Account_AccountGroup_Parent.ToString()
                            }
                        };

                        nodesList.Add(rootNode);
                        PopulateTree(accounts, rootNode, inFormAccess);
                    }
                }
            }

            return Json(nodesList);
        }
        public void PopulateTree(List<AccountList> accounts, JsTreeNode parentNode, InFormAccess inFormAccess)
        {
            foreach (AccountList account in accounts)
            {
                if (account.Account_AccountGroup_Parent == parentNode.id.ToInt())
                {
                    //string jstree_actions = "<div class='btn-group account-accounts'>" +
                    //        (inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn btn-success btn-sm add-account-ledger'><i class='ti-plus'></i></button>" : "") +
                    //        "</div>";
                    //string title = account.Account_Node_Name;

                    //if (account.Account_Is_Group == 1)
                    //{
                    //    title = "<span style='font-size: 0.8rem; font-weight: bold'>" + title + "</span>";
                    //}

                    //if (account.Account_Is_Group != 1)
                    //{
                    //    jstree_actions = "<div class='btn-group account-accounts'>" +
                    //        (inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn btn-warning btn-sm edit-account-ledger'><i class='ti-pencil'></i></button>" : "") +
                    //        (inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn btn-danger btn-sm remove-account-ledger'><i class='ti-trash'></i></button>" : "") +
                    //        "</div>";
                    //}


                    var node = new JsTreeNode()
                    {
                        id = account.Account_Node_Id.ToString(),
                        //text = "<div>" + title + jstree_actions + "</div>",
                        text = "<span "+ ((account.Account_Is_Group == 1)? " style = 'font-size: 0.8rem; font-weight: bold' ":"") + ">" + account.Account_Node_Name + "</span>" + "<div id=div_" + account.Account_Node_Id.ToString() + " class='account-accounts' style='float:right;padding-right:15px'>  " +
                             "<button type='button' class='btn  btn-info' style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'view')\"><i class='fa fa-info'> </i></button>" +
                         ( inFormAccess.Insert_Row > 0 ? "<button type='button' class='btn  btn-success'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'new')\"><i class='fa fa-file'></i> </button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Edit_Row > 0 ? "<button type='button' class='btn  btn-warning'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'edit')\"><i class='fa fa-pencil'></i> </button>" : "") +
                            (account.Account_Is_Group != 1 && inFormAccess.Delete_Row > 0 ? "<button type='button' class='btn  btn btn-danger'  style='margin-right:3px' onclick=\"showAccountPopup(" + account.Account_Node_Id.ToString() + ",'delete')\"><i class='fa fa-trash'></i></button>" : "") +

                         "</div>",
                        li_attr = new JsTreeNodeLiAttributes()
                        {
                            data = account.Account_AccountGroup_Parent.ToString()
                        }
                    };

                    parentNode.children.Add(node);
                    PopulateTree(accounts, node, inFormAccess);
                }
            }

        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {


            ResultDto result = _deleteAccountLedgerService.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                ResultDto<InFormAccess> dto = this._getAccessService.Execute(activeUser.CompanyUsers_Id);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", dto.Data) });
            }
            else
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
