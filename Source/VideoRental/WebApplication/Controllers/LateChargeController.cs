using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class LateChargeController : Controller
    {

        ILateChargesServices iLateChargesServices;

        // GET: LateCharge
        // Show customer has late charge
        public ActionResult ShowLateCharge()
        {
            return View(iLateChargesServices.FindCustomersHasLateCharge());
        }


    }
}