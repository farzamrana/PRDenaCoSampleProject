using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Utilities
{
    public static class CurrentUser
    {
        
        public static ActiveUser Get()
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            return SessionExtension.GetObject<ActiveUser>(httpContextAccessor.HttpContext.Session, "ActiveUser");
          
                
        }
        public static void Set(ActiveUser activeUser)
        {
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            SessionExtension.SetObject(httpContextAccessor.HttpContext.Session, "ActiveUser", activeUser);


        }
    }
}
