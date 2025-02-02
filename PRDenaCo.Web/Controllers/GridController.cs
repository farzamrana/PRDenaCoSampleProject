﻿using PRDenaCo.Application.Services.Common.Queries.GetListItem;
using PRDenaCo.Web.Models.Grid;
using PRDenaCo.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Controllers
{
    public class GridController : Controller
    {
        private readonly IGetListItemService _getListItemService;
        public GridController(IGetListItemService getListItemService)
        {
            this._getListItemService = getListItemService;
        }
        public IActionResult Index()
        {
            List<GridModel> gm = new List<GridModel>() { new GridModel() {
                Category_Id = 2,
            Id = 1,
            Name = "Book1",
            AuthorName = "Author1",
            AccountGroup_Id=1
            },
            new GridModel() {
                Category_Id = 1,
            Id = 2,
            Name = "Book2",
            AuthorName = "Author2",
            AccountGroup_Id=2
            },new GridModel(){Id=0}
            };
            ViewBag.Categories = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);


            return View(gm);
        }
        [HttpPost]
        public IActionResult Index(IEnumerable<GridModel> model)
        {

            ViewBag.Categories = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);
            ViewBag.AccountGroups = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);

            return View(model);
        }
    }
}
