﻿using PRDenaCo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using PRDenaCo.Web.Utility;
using PRDenaCo.Application.Services.Products.Queries.GetCompanies;
using PRDenaCo.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static PRDenaCo.Web.Helper;
using PRDenaCo.Web.Models.Companies;
using PRDenaCo.Web.Utilities;
using PRDenaCo.Application.Services.Common.Queries.GetCurrentDate;
using PRDenaCo.Common.Dtos;

namespace PRDenaCo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IGetCurrentDateService _getCurrentDateService;


        public HomeController(ILogger<HomeController> logger, IGetCurrentDateService getCurrentDateService)
        {
            _logger = logger;
            _getCurrentDateService = getCurrentDateService;
        }

        public IActionResult Index()
        {
           // _logger.LogError(LogEvent.ProcessStarted, "ProcessStarted ...");
            return View();
        }
        [HttpGet]
        public IActionResult Dashboard(string serchkey, int page = 1)
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);
          
            return View();
        }
        [HttpGet]
        public IActionResult DashboardTwo()
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);

            return View();
        }
        public IActionResult DashboardThree()
        {
            ViewData["IsRendred"] = this.CheckIsRendred(this.Request);

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ResetCurrentDate()
        {
            ActiveUser activeUser = CurrentUser.Get();
           ResultDto<DateTime> result= _getCurrentDateService.Execute(activeUser.Company_Id);
            CurrentDateModel model = new CurrentDateModel() { CurrentDate = result.Data.ToCustomFormat(activeUser.DateFormats_Description) };
            return Json(model.CurrentDate);
        }
            [NoDirectAccess]
        public IActionResult SetCurrentDate()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            CurrentDateModel model = new CurrentDateModel();
          //  ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
                model.CurrentDate = activeUser.WorkDay.ToCustomFormat(activeUser.DateFormats_Description);
            return View(model);
        }
        public IActionResult GetCurrentDate()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            CurrentDateModel model = new CurrentDateModel();
            //  ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            model.CurrentDate = activeUser.WorkDay.ToCustomFormat(activeUser.DateFormats_Description);
            return Json(model.CurrentDate);
        }
        [HttpPost]
       
        public IActionResult SetCurrentDate(string currentDate)
        {

            CurrentDateModel model = new CurrentDateModel();
            model.CurrentDate = currentDate;
            ActiveUser activeUser = CurrentUser.Get();
            activeUser.WorkDay = currentDate.ToDate();
            if (currentDate.ToDate() == DateTime.Now.ToDate())
                activeUser.IsCurrentDate = true;
            else
                activeUser.IsCurrentDate = false;

            CurrentUser.Set(activeUser);

            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            return Json(model.CurrentDate);
        }


    }
}
