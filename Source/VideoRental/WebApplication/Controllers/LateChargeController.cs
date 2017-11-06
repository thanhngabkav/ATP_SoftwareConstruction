using DataAccess.Entities;
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

        public LateChargeController(ILateChargesServices iLateChargesServices)
        {
            this.iLateChargesServices = iLateChargesServices;
        }

        private int currentCustomerID;
        private const string CUSTOMER_SESSION = "currentCustomerID";

        // Show customer has late charge
        /// <summary>
        /// LateChargeManagement
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            TagDebug.D(GetType(), " in Action " + "ShowLateCharge");
            return View(iLateChargesServices.FindCustomersHasLateCharge());
        }

        [HttpGet]
        public ActionResult RecordLateCharge(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "RecordLateCharge GET");

            int currentCustomerID = 0;
            if (customerID == 0)
                currentCustomerID = (int)Session[CUSTOMER_SESSION];
            else
            {
                Session[CUSTOMER_SESSION] = customerID;
                currentCustomerID = customerID;
            }
            ViewBag.numberLateCharge = iLateChargesServices.GetNumberOfLateCharge(currentCustomerID);
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
            int customerID = (int)Session[CUSTOMER_SESSION];
            int numberLatecharge = iLateChargesServices.GetNumberOfLateCharge(customerID);

            if (enoughForRecordlateCharge(numberLatecharge, numberRequest))
            {
                iLateChargesServices.RecordLateCharge(customerID, numberRequest.number);
                ViewBag.Success = "Ghi Nhận Trễ Hạn Thành công";
            }
            else
            {
                ViewBag.Success = "Không đủ để xóa";
            }
            ViewBag.numberLateCharge = iLateChargesServices.GetNumberOfLateCharge(currentCustomerID);
            return View();
        }

        private bool enoughForRecordlateCharge(int numberLatecharge, NumberRequestView numberRequest)
        {
            return numberLatecharge >= numberRequest.number && numberLatecharge > 0 ? true : false;
        }

        [HttpGet]
        public ActionResult CancelLateCharge(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
            currentCustomerID = customerID;
            return View(iLateChargesServices.GetAllLateChargeOfCustomer(customerID));
        }

        /// <summary>
        /// Change status of Transaction History
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CancelASpecificLateCharge(int transactionID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
            int customerID = currentCustomerID;
            iLateChargesServices.CancelLateCharge(transactionID);
            return RedirectToAction("CancelLateCharge");
        }

    }
}