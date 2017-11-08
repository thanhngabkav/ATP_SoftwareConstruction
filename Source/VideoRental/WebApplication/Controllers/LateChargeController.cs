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

        private const string CUSTOMER_SESSION = "currentCustomerID";

        // Show customer has late charge
        /// <summary>
        /// LateChargeManagement
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string status)
        {
            ViewBag.status = status;
            TagDebug.D(GetType(), " in Action " + "ShowLateCharge");
            return View(iLateChargesServices.FindCustomersHasLateCharge());
        }

        [HttpGet]
        public ActionResult RecordLateCharge(int customerID, string status)
        {
            TagDebug.D(GetType(), " in Action " + "RecordLateCharge GET");
            ViewBag.status = status;
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
        [HttpGet]
        public ActionResult RecordASpecificLateCharge(int numberRequest)
        {
            TagDebug.D(GetType(), " in Action " + "RecordASpecificLateCharge");
            int customerID = (int)Session[CUSTOMER_SESSION];
            iLateChargesServices.RecordLateCharge(customerID, numberRequest);
            return RedirectToAction("RecordLateCharge", new { customerID =customerID, status ="Ghi Nhận trễ hạn thành công"});
        }

        private bool IsEnoughForRecordlateCharge(int numberLatecharge, NumberRequestView numberRequest)
        {
            return numberLatecharge >= numberRequest.number && numberLatecharge > 0 && numberRequest.number > 0 ? true : false;
        }

        [HttpGet]
        public ActionResult CancelLateCharge(int customerID)
        {
            TagDebug.D(GetType(), " in Action " + "CancelLateCharge ");
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
            TagDebug.D(GetType(), " in Action " + "CancelASpecificLateCharge ");
            iLateChargesServices.CancelLateCharge(transactionID);
            return RedirectToAction("Index", new { status = "Hủy Trễ Hạn Thành Công"});
        }

        [HttpPost]
        public ActionResult LateChargePayment(NumberRequestView numberRequest)
        {
            int customerID = (int)Session[CUSTOMER_SESSION];
            int numberLatecharge = iLateChargesServices.GetNumberOfLateCharge(customerID);
            string status = "";
            if (IsEnoughForRecordlateCharge(numberLatecharge, numberRequest))
            {
                ViewBag.LateChargePayment = iLateChargesServices.GetTotalLateChargePrice(customerID, numberRequest.number);
                return View(numberRequest);
            }
            return RedirectToAction("RecordLateCharge", new { customerID = customerID, status = "Không đủ để xóa" });
        }

    }
}