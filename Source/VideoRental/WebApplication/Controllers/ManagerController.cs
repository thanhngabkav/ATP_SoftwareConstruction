using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ManagerController : Controller
    {
        [Authorize(Roles = "Manager")]
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
    }
}