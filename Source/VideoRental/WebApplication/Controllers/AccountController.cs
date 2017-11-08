using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models;
using WebApplication.Services;
using DataAccess.Entities;

namespace WebApp.Controllers
{
    [UserAuthActionFilter]
    public class AccountController : Controller
    {
        // GET: Account
        /**
         * Show user's information
         * */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Show Login page
         * */
        [HttpGet]
        public ActionResult Login()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/Home");
            }
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            AccountService accountService = new AccountService();
            if (ModelState.IsValid && loginModel.Username != null && accountService.login(loginModel))
            {
                UserService userService = new UserService();
                User user = userService.getUserByUserName(loginModel.Username);
                UserSession userSession = new UserSession { UserID = user.UserID + "", UserName = user.UserName, PreviusURL = "/Account/Login", UserRole = user.Role };
                Session.Add(UserSession.SessionName, userSession);
                FormsAuthentication.SetAuthCookie(user.UserName, loginModel.Remember);
                if (loginModel.Remember)
                {
                    var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddDays(10), true, user.Role);
                    HttpCookie userCookies = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    userCookies.Expires = authTicket.Expiration;
                    Response.Cookies.Add(userCookies);
                }
                return Redirect("/Home");
            }
            else
            {
                ViewBag.LoginErrorMess = "Tên đăng nhập hoặc mật khẩu không đúng.\n Vui lòng đăng nhập lại!";
                return View();
            }

        }
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Request.Cookies.Clear();
            Session[UserSession.SessionName] = null;
            Session.Clear();
            return Redirect("/Home");
        }
    }
}