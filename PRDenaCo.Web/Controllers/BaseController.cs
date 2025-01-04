using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool CheckIsRendred(HttpRequest request)
        {
            try
            {
                StringValues queryVal;

                if (request.Query.TryGetValue("IsRendred", out queryVal))
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }
    }
}
