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
        //private static string RETURNDISK_SESSION = "returndisk";

        IRentAndReturnDiskService iRentAndReturnDiskService;
        // GET: RentAndReturnDisks

        public RentAndReturnDisksController(IRentAndReturnDiskService iRentAndReturnDiskService)
        {
            this.iRentAndReturnDiskService = iRentAndReturnDiskService;
        }

        /// <summary>
        /// Retal And Return Management
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Index(string status)
        {
            TagDebug.D(GetType(), " in Action " + "RetalAndReturnManagement");
            ViewBag.status = status;
            return View();
        }


        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowAllDisk(string diskName, string status)
        {
            TagDebug.D(GetType(), " in Action " + "ShowAllDisk");
            ViewBag.status = status;
            if (diskName == null) diskName = "";
            IList<Disk> disks = iRentAndReturnDiskService.GetDisks(diskName);
            IList<DiskView> diskViews = new List<DiskView>();

            foreach (Disk aDisk in disks)
            {
                DiskTitle diskTitle = iRentAndReturnDiskService.GetDiskTitleName(aDisk.TitleID);

                diskViews.Add(new DiskView(aDisk.DiskID, diskTitle.Title, aDisk.PurchasePrice, "", aDisk.Status));
            }
            return View(diskViews);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult SaveListDisk(int[] diskID)
        {
            TagDebug.D(GetType(), " in Action " + "SaveListDisk");
            foreach (int a in diskID)
                TagDebug.D(GetType(), "DIsk ID = " + a);

            Session[RENTING_SESSION] = diskID;
            if (diskID != null)
                return RedirectToAction("ShowAllCustomer");
            else
                return RedirectToAction("ShowAllDisk");
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowAllCustomer(string customerName, string status)
        {
            TagDebug.D(GetType(), "in Action " + "ShowAllCustomer");
            ViewBag.status = status;
            if (customerName == null) customerName = "";
            IList<Customer> customers = iRentAndReturnDiskService.GetCustomers(customerName);
            IList<CustomerView> customerViews = new List<CustomerView>();
            foreach (Customer aCustomer in customers)
                customerViews.Add(new CustomerView(aCustomer.CustomerID, aCustomer.FirstName, aCustomer.LastName, aCustomer.Address));
            return View(customerViews);
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult PaymentDisk(int? id)
        {
            TagDebug.D(GetType(), " in Action " + "PaymentDisk" + id);
            int[] diskID = (int[])Session[RENTING_SESSION];
            Session[CUSTOMER_SESSION] = id;

            if (diskID.Length > 0 && id != null)
            {
                IList<DiskPriceView> diskPriceViews = iRentAndReturnDiskService.GetPriceEachDisk(diskID);
                float total = 0;
                foreach(DiskPriceView d in diskPriceViews)
                    total += d.price;
                ViewBag.Total = total;
                return View(diskPriceViews);
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
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult WriteRentingDisk()
        {
            TagDebug.D(GetType(), " in Action " + "WriteRentingDisk");
            int[] diskID = (int[])Session[RENTING_SESSION];
            int customerID = (int)Session[CUSTOMER_SESSION];
            int userID = Int32.Parse((string)Session[UserSession.SessionName]); // test set default = 1
            if (diskID.Length > 0 && customerID != 0)
            {
                if (iRentAndReturnDiskService.CheckDiskCanBeRented(diskID, customerID)) 
                    iRentAndReturnDiskService.WriteRentalDisk(diskID, customerID, userID);
                else 
                    return RedirectToAction("ShowAllCustomer", new { status = "Khách Hàng Này Không Đặt Đĩa Này"});
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 ");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }
            return RedirectToAction("ShowAllDisk", new { status = "Thanh Toán Thành Công" });
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ReturnDisk(string diskID)
        {
            if (diskID == null) diskID = "";
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            List<DiskView> dv = new List<DiskView>();
            IList<Disk> rentedDisk = iRentAndReturnDiskService.GetRentedDisks(diskID);
            foreach (Disk d in rentedDisk)
            {
                DiskTitle t = iRentAndReturnDiskService.GetDiskTitleName(d.TitleID);
                dv.Add(new DiskView(d.DiskID, t.Title, d.PurchasePrice, "", d.Status));
            }
            return View(dv);
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ReturnASpecificDisk(int id)
        {
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            string status = "";
            if (id > 0)
            {
                iRentAndReturnDiskService.ReturnDisks(id);
                status = "Trả Đĩa Thành Công";
            }
            else
            {
                TagDebug.D(GetType(), " diskID is Null");
                status = "Trả Đĩa Thành Công";
                // Handle Exeption
            }
            return RedirectToAction("Index", new { status = status });
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowLateCharge()
        {
            return RedirectToAction("Index", "LateCharge", "");
        }
    }
}