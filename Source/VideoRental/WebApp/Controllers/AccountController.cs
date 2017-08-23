using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;
using WebApp.Services;
using DAL.Entities;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize]
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
            UserSession userSession = (UserSession)Session[UserSession.SessionName];
            if (userSession != null)
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
                UserSession userSession = new UserSession { UserID = user.UserID+"", UserName = user.UserName, PreviusURL="/Account/Login" };
                Session.Add(UserSession.SessionName, userSession);
                if (loginModel.Remember)
                {
                    HttpCookie userCookies = new HttpCookie("userAuth");
                    userCookies.Value = loginModel.Username;
                    userCookies.Expires = DateTime.Now.AddHours(200);
                    Response.SetCookie(userCookies);
                    Response.Flush();
                }
                return Redirect("/Home");
            }
            else
            {
                ViewBag.LoginErrorMess = "Tên đăng nhập hoặc mật khẩu không đúng.\n Vui lòng đăng nhập lại!";
                return View();
            }
            
        }
    }
}