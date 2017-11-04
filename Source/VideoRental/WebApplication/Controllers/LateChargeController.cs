using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class LateChargeController : Controller
    {

        ILateChargesServices iLateChargesServices;

        private string currentCustomerID;
        // GET: LateCharge
        // Show customer has late charge
        public ActionResult ShowLateCharge()
        {
            TagDebug.D(GetType(), " in Action " + "ShowLateCharge");
            return View(iLateChargesServices.FindCustomersHasLateCharge());
        }

        [HttpGet]
        public ActionResult RecordLateCharge(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "RecordLateCharge GET");
            currentCustomerID = customerID;
            return View();
        }


        /// <summary>
        /// number Record want to pay
        /// </summary>
        /// <param name="numberRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RecordLateCharge(NumberRequestView numberRequest)
        {
            TagDebug.D(GetType(), " in Action " + "RecordLateCharge POST");
            string customerID = currentCustomerID;
            iLateChargesServices.RecordLateCharge(customerID, numberRequest.number);
            return View();
        }

        [HttpGet]
        public ActionResult ShowLateCharge(string customerID)
        {

            TagDebug.D(GetType(), " in Action " + "ShowLateCharge");
            if (customerID != null)
                return View(iLateChargesServices.ShowLateCharge(customerID));
            else
            {
                TagDebug.D(GetType(), " Customer ID is null ");
            }
            return View();
        }


        [HttpGet]
        public ActionResult CancelLateCharge(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
            currentCustomerID = customerID;
            return View();
        }

        /// <summary>
        /// Number of LateCharge want to cancel
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancelLateCharge(NumberRequestView numberRequest)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
            string customerID = currentCustomerID;
            iLateChargesServices.CancelLateCharge(customerID, numberRequest.number);
            return View();
        }

    }
}