using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models;

namespace WebApplication.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                UserService userService = new UserService();

                HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                string cookiePath = ticket.CookiePath;
                DateTime expiration = ticket.Expiration;
                bool expired = ticket.Expired;
                bool isPersistent = ticket.IsPersistent;
                DateTime issueDate = ticket.IssueDate;
                string name = ticket.Name;
                string userData = ticket.UserData;
                User user = userService.getUserByUserName(name);
                UserSession userSession = new UserSession { UserID = user.UserID + "", UserName = user.UserName, PreviusURL = "/Account/Login", UserRole = user.Role };
                filterContext.HttpContext.Session.Add(UserSession.SessionName, userSession);
                int version = ticket.Version;
            }

        }
    }
}