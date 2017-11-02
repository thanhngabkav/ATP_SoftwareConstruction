using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using DataAccess.Entities;
using DataAccess.Utilities;

namespace WebApplication.Controllers
{
    public class RentAndReturnDisksController : Controller
    {
        private static string RENTING_SESSION = "renting";
        private static string CUSTOMER_SESSION = "customerid";
        private static string RETURNDISK_SESSION = "returndisk";
        IRentAndReturnDiskService iRentAndReturnDiskService;
        // GET: RentAndReturnDisks

        [HttpGet]
        public ActionResult RetalAndReturnManagement()
        {
            TagDebug.D(GetType(), " in Action " + "RetalAndReturnManagement");
            return View();
        }


        [HttpGet]
        public ActionResult ShowAllDisk(string diskName)
        {
            TagDebug.D(GetType(), " in Action " + "ShowAllDisk");
            return View(iRentAndReturnDiskService.GetDisks(diskName));
        }

        [HttpPost]
        public ActionResult SaveListDisk(string[] diskID)
        {
            TagDebug.D(GetType(), " in Action " + "SaveListDisk");
            Session[RENTING_SESSION] = diskID;
            return View(iRentAndReturnDiskService.GetDisks(""));
        }

        [HttpGet]
        public ActionResult ShowAllCustomer(string customerName)
        {
            TagDebug.D(GetType(), " in Action " + "ShowAllCustomer");
            return View(iRentAndReturnDiskService.GetCustomers(customerName));
        }

        [HttpPost]
        public ActionResult PaymentDisk(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "PaymentDisk");
            string[] diskID = (string[])Session[RENTING_SESSION];
            Session[CUSTOMER_SESSION] = customerID;

            if (diskID.Length > 0 && customerID != null)
            {
                return View(iRentAndReturnDiskService.GetPriceEachDisk(diskID));
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 " + "");
                if (customerID == null)
                    TagDebug.D(GetType(), " customerID Null " + "");
                // Handle NULL POINTER
            }
            return View();
        }


        [HttpPost]
        public ActionResult WriteRentingDisk()
        {
            TagDebug.D(GetType(), " in Action " + "WriteRentingDisk");
            string[] diskID = (string[])Session[RENTING_SESSION];
            string customerID = (string)Session[CUSTOMER_SESSION];
            string userID = "";
            if (diskID.Length > 0 && customerID != null)
            {
                iRentAndReturnDiskService.WriteRentalDisk(diskID, customerID, userID);
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 ");
                if (customerID != null)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }
            return View();
        }

        [HttpGet]
        public ActionResult ReturnDisk(string diskID)
        {
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            return View(iRentAndReturnDiskService.GetRentedDisks(diskID));
        }

        [HttpPost]
        public ActionResult ReturnDisk(string[] diskID)
        {
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            if (diskID.Length > 0)
            {
                iRentAndReturnDiskService.ReturnDisks(diskID);
            }
            else
            {
                TagDebug.D(GetType(), " diskID is Null");
                // Handle Exeption
            }
            return View();
        }

        [HttpGet]
        public ActionResult ShowLateCharge(string customerID)
        {
            TagDebug.D(GetType(), " in Action " + "ShowLateCharge ");
            if (customerID != null)
                return View(iRentAndReturnDiskService.ShowLateCharge(customerID));
            else
            {
                TagDebug.D(GetType(), " Customer ID is null ");
            }
            return View();// return to show late charege in showlatecharge.cshtml
        }
    }
}