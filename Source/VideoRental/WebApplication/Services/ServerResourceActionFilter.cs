using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Models;
using WebApplication.Services;
using DataAccess.Entities;

namespace WebApplication.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ServerResourceActionFilter : ActionFilterAttribute
    {
        /**
         * Intercept request to Server Resource
         * */
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null || filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error" }));
            }
        }
    }
}