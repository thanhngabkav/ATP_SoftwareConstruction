using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using DataAccess.Entities;
using DataAccess.Utilities;
using WebApplication.Models;
using System.Web.Security;

namespace WebApplication.Controllers
{

    // [Authorize(Roles = "Manager")]
    public class RentAndReturnDisksController : Controller
    {
        private static string RENTING_SESSION = "renting";
        private static string CUSTOMER_SESSION = "customerid";
        private static string RETURNDISK_SESSION = "returndisk";

        IRentAndReturnDiskService iRentAndReturnDiskService;
        // GET: RentAndReturnDisks

        public RentAndReturnDisksController(IRentAndReturnDiskService iRentAndReturnDiskService)
        {
            this.iRentAndReturnDiskService = iRentAndReturnDiskService;
        }

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
            if (diskName == null) diskName = "";
            IList<Disk> disks = iRentAndReturnDiskService.GetDisks(diskName);
            IList<DiskView> diskViews = new List<DiskView>();

            foreach (Disk aDisk in disks)
            {
                DiskTitle diskTitle = iRentAndReturnDiskService.getDiskTitleName(aDisk.TitleID);

                diskViews.Add(new DiskView(aDisk.DiskID, diskTitle.Title, aDisk.PurchasePrice, "", aDisk.Status));
            }
            return View(diskViews);
        }

        [HttpPost]
        public ActionResult SaveListDisk(string[] diskID)
        {
            TagDebug.D(GetType(), " in Action " + "SaveListDisk");
            foreach (string a in diskID)
                TagDebug.D(GetType(), "DIsk ID = " + a);

            Session[RENTING_SESSION] = diskID;
            return RedirectToAction("ShowAllCustomer");
        }

        [HttpGet]
        public ActionResult ShowAllCustomer(string customerName)
        {
            TagDebug.D(GetType(), "in Action " + "ShowAllCustomer");
            if (customerName == null) customerName = "";
            IList<Customer> customers = iRentAndReturnDiskService.GetCustomers(customerName);
            IList<CustomerView> customerViews = new List<CustomerView>();
            foreach (Customer aCustomer in customers)
                customerViews.Add(new CustomerView(aCustomer.CustomerID, aCustomer.FirstName, aCustomer.LastName, aCustomer.Address));

            return View(customerViews);
        }

        [HttpGet]
        public ActionResult PaymentDisk(int? id)
        {
            TagDebug.D(GetType(), " in Action " + "PaymentDisk" + id);
            string[] diskID = (string[])Session[RENTING_SESSION];
            Session[CUSTOMER_SESSION] = id;

            if (diskID.Length > 0 && id != null)
            {
                return View(iRentAndReturnDiskService.GetPriceEachDisk(diskID));
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 " + "");
                if (id == null)
                    TagDebug.D(GetType(), " customerID Null " + "");
                // Handle NULL POINTER
            }
            return View();
        }


        [HttpGet]
        public ActionResult WriteRentingDisk()
        {
            TagDebug.D(GetType(), " in Action " + "WriteRentingDisk");
            string[] diskID = (string[])Session[RENTING_SESSION];
            int customerID = (int)Session[CUSTOMER_SESSION];
            int userID = 1; // test set default = 1
            if (diskID.Length > 0 && customerID != 0)
            {
                iRentAndReturnDiskService.WriteRentalDisk(diskID, customerID, userID);
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 ");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }
            return RedirectToAction("ShowAllDisk");
        }

        [HttpGet]
        public ActionResult ReturnDisk(string diskID)
        {
            if (diskID == null) diskID = "";
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            List<DiskView> dv = new List<DiskView>();
            IList<Disk> rentedDisk = iRentAndReturnDiskService.GetRentedDisks(diskID);
            foreach(Disk d in rentedDisk)
            {
                DiskTitle t = iRentAndReturnDiskService.getDiskTitleName(d.TitleID);
                dv.Add(new DiskView(d.DiskID, t.Title, d.PurchasePrice, "", d.Status));
            }
            return View(dv);
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