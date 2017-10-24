using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using DataAccess.Entities;
namespace WebApplication.Controllers
{
    public class RentAndReturnDisksController : Controller
    {
        private static string RENTING_SESSION = "renting";
        private static string CUSTOMER_SESSION = "customerid";
        private static string TAG = "RentAndReturnDisksController: ";
        IRentAndReturnDiskService iRentAndReturnDiskService;
        // GET: RentAndReturnDisks

        [HttpGet]
        public ActionResult RetalAndReturnManagement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentDisk(string customerID)
        {
            Console.WriteLine(TAG + " in Action " + "PaymentDisk");
            string[] diskID = (string[])Session[RENTING_SESSION];
            Session[CUSTOMER_SESSION] = customerID;

            if (diskID.Length > 0 && customerID != null)
            {
                return View(iRentAndReturnDiskService.GetPriceEachDisk(diskID));
            }
            else
            {
                if (diskID.Length <= 0)
                    Console.WriteLine(TAG + " List of Disk < 0 " + "");
                if (customerID == null)
                    Console.WriteLine(TAG + " customerID Null " + "");
                // Handle NULL POINTER
            }
            return View();
        }

        [HttpGet]
        public ActionResult ShowAllDisk(string diskName)
        {
            Console.WriteLine(TAG + " in Action " + "ShowAllDisk");
            return View(iRentAndReturnDiskService.GetDisks(diskName));
        }

        [HttpPost]
        public ActionResult SaveListDisk(string[] diskID)
        {
            Console.WriteLine(TAG + " in Action " + "SaveListDisk");
            Session[RENTING_SESSION] = diskID;
            return View(iRentAndReturnDiskService.GetDisks(""));
        }

        [HttpGet]
        public ActionResult ShowAllCustomer(string customerName)
        {
            Console.WriteLine(TAG + " in Action " + "ShowAllCustomer");
            return View(iRentAndReturnDiskService.GetCustomers(customerName));
        }

        [HttpPost]
        public ActionResult WriteRentingDisk()
        {
            Console.WriteLine(TAG + " in Action " + "WriteRentingDisk");
            string[] diskID = (string[])Session[RENTING_SESSION];
            string customerID = (string)Session[CUSTOMER_SESSION];
            if (diskID.Length > 0 && customerID != null)
            {
                iRentAndReturnDiskService.WriteRentingDisk(diskID, customerID);
            }
            else
            {
                if (diskID.Length <= 0)
                    Console.WriteLine(TAG + " List of Disk < 0 ");
                if (diskID.Length <= 0)
                    Console.WriteLine(TAG + " customerID Null ");

                // Handle Exeption
            }

            return View();
        }


    }
}