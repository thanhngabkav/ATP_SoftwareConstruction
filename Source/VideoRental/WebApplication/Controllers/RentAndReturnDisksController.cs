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
        private const string RENTING_SESSION = "renting";
        private const string CUSTOMER_SESSION = "customerid";
        private const string DISK_CHOSEN_SESSION = "diskchosen";

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
        public ActionResult ShowAllDisk(string diskName, string ma)
        {
            TagDebug.D(GetType(), " in Action " + "ShowAllDisk");
            ViewBag.diskName = diskName;

            if (diskName == null) diskName = "";
            IList<Disk> disks = iRentAndReturnDiskService.GetDisks(diskName);
            IList<DiskView> diskViews = new List<DiskView>();
            foreach (Disk aDisk in disks)
            {
                DiskTitle diskTitle = iRentAndReturnDiskService.GetDiskTitleName(aDisk.TitleID);
                diskViews.Add(new DiskView(aDisk.DiskID, diskTitle.Title, aDisk.PurchasePrice, "", aDisk.Status));
            }

            int id = 0;
            if (ma != null)
            {
                id = Int32.Parse(ma);
                if (Session[DISK_CHOSEN_SESSION] == null)
                {
                    Session[DISK_CHOSEN_SESSION] = new List<Int32>
                {
                    id
                };
                }
                else
                {
                    List<Int32> chosenDisk = (List<Int32>)Session[DISK_CHOSEN_SESSION];
                    if (chosenDisk.Any(x => x == id))
                        chosenDisk.Remove(chosenDisk.SingleOrDefault(x => x == id));
                    else
                        chosenDisk.Add(id);
                    Session[DISK_CHOSEN_SESSION] = chosenDisk;
                }
            }

            List<Int32> rentedList = (List<Int32>)Session[DISK_CHOSEN_SESSION];
            if (rentedList != null)
                foreach (int data in rentedList)
                    if (diskViews.Any(x => x.DiskID == data))
                        diskViews.Where(x => x.DiskID == data).First().IsChosen = !diskViews.Where(x => x.DiskID == data).First().IsChosen;

            return View(diskViews);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult SaveListDisk()
        {
            TagDebug.D(GetType(), " in Action " + "SaveListDisk");
            List<Int32> listDisk = (List<Int32>)Session[DISK_CHOSEN_SESSION];
            if (listDisk != null)
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
            int[] diskID = ((List<Int32>)Session[DISK_CHOSEN_SESSION]).ToArray();
            Session[CUSTOMER_SESSION] = id;

            if (diskID.Length > 0 && id != null)
            {
                IList<DiskPriceView> diskPriceViews = iRentAndReturnDiskService.GetPriceEachDisk(diskID);
                float total = 0;
                foreach (DiskPriceView d in diskPriceViews)
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
            int[] diskID = ((List<Int32>)Session[DISK_CHOSEN_SESSION]).ToArray();
            int customerID = (int)Session[CUSTOMER_SESSION];
            UserSession userSession = (UserSession)Session[UserSession.SessionName];
            int userID = Int32.Parse(userSession.UserID); // test set default = 1
            if (diskID.Length > 0 && customerID != 0)
            {
                if (iRentAndReturnDiskService.CheckDiskCanBeRented(diskID, customerID))
                    iRentAndReturnDiskService.WriteRentalDisk(diskID, customerID, userID);
                else
                    return RedirectToAction("ShowAllCustomer", new { status = "Khách Hàng Này Không Đặt Đĩa Này" });
            }
            else
            {
                if (diskID.Length <= 0)
                    TagDebug.D(GetType(), " List of Disk < 0 ");
                if (customerID != 0)
                    TagDebug.D(GetType(), " customerID Null ");
                // Handle Exeption
            }
            if (iRentAndReturnDiskService.CheckCustomerLateCharge(customerID))
            {
                ViewBag.status = "Thanh Toán Thành Công";
                ViewBag.customerID = customerID;
                ResetDiskChosen();
                return View();
            }
            ResetDiskChosen();
            return RedirectToAction("Index", new { status = "Thanh Toán Thành Công" });
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
        public ActionResult ReturnASpecificDisk(int id, string returnDate)
        {
            TagDebug.D(GetType(), " in Action " + "ReturnDisk GET");
            
            string status = "";
            if (id > 0)
            {
                iRentAndReturnDiskService.ReturnDisks(id, returnDate);
                status = "Trả Đĩa Và Xử Lý Trễ Hạn Thành Công";
                ResetDiskChosen();
            }
            else
            {
                TagDebug.D(GetType(), " diskID is Null");
                status = "Trả Đĩa Và Xử Lý Trễ Hạn Thành Công";
                ResetDiskChosen();
                // Handle Exeption
            }
            
            return RedirectToAction("Index", new { status = status });
        }
        

        private void ResetDiskChosen()
        {
            Session[DISK_CHOSEN_SESSION] = null;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowLateCharge()
        {
            return RedirectToAction("Index", "LateCharge", "");
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult ShowChosenDisk()
        {
            IList<Int32> listDisk = (IList<Int32>)Session[DISK_CHOSEN_SESSION];
            if (listDisk == null) return new EmptyResult();
            if (listDisk.Count > 0)
            {

                IList<DiskView> diskViews = new List<DiskView>();
                foreach (int diskID in listDisk)
                {
                    Disk disks = iRentAndReturnDiskService.GetADisk(diskID);
                    DiskTitle diskTitle = iRentAndReturnDiskService.GetDiskTitleName(disks.TitleID);
                    diskViews.Add(new DiskView(disks.DiskID, diskTitle.Title, disks.PurchasePrice, "", disks.Status));
                }
                return PartialView(diskViews);
            }
            return null;

        }
    }
}