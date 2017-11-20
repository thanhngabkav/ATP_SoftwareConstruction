using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using DataAccess.Entities;
using WebApplication.Models;
using DataAccess.Utilities;
namespace WebApplication.Controllers
{
    public class DiskController : Controller
    {
        private IDiskManagementService diskManagement;
        private IDiskService db;
        private IDiskTitleService dbDiskTitle;


        public DiskController(IDiskManagementService diskManagement, IDiskService diskService, IDiskTitleService dbDiskTitle)
        {
            this.diskManagement = diskManagement;
            this.db = diskService;
            this.dbDiskTitle = dbDiskTitle;

        }

        // GET: Disk
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Index()
        {
            var disks = db.GetAllDisks();
            return View(disks.ToList());
        }

        // GET: Disk/Details
        public ActionResult Details(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // GET: Disk/Create
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Create()
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            return View();
        }

        // POST: Disk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Create([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,DateUpdate,DateCreate")] Disk disk, int Number)
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            // Check Number
            if (!CheckNumber(Number))
            {
                ViewBag.number = "Number is not valid";
                return View(disk);
            }
            else
            {
                ViewBag.number = "";
            }
            
            bool flag = false;
            for (int i = 0; i < Number; i++)
            {
                // Check PurchasePrice
                if (!CheckPurchasePrice(disk.PurchasePrice))
                {
                    ViewBag.purchasePrice = "Purchase Price is not valid";
                    return View(disk);
                }
                else
                {
                    ViewBag.purchasePrice = "";
                }
                
                disk.Status = "RENTABLE";
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                disk.UpdatedUser = Int32.Parse(userSession.UserID);
                disk.DateCreate = DateTime.Now;
                disk.DateUpdate = DateTime.Now;
                disk.RentedTime = 0;
                disk.LastRentedDate = null;
                db.AddNewDisk(disk);
                flag = true;
            }
            if (flag)
            {
                ViewBag.ok = "Thêm thành công";
                return View("Success");
            }

            ViewBag.ok = "Thêm không thành công";
            return View("Failure");
        }

        // GET: Disk/Edit
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Edit(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            List<SelectListItem> listItems = new List<SelectListItem>();
           listItems.Add(new SelectListItem
           {
               Text = "RENTABLE",
               Value = "RENTABLE"
           });
           listItems.Add(new SelectListItem
           {
               Text = "BOOKED",
               Value = "BOOKED"
           });
           listItems.Add(new SelectListItem
           {
               Text = "RENTED",
               Value = "RENTED"
           });
           ViewBag.ListStatus = new SelectList(listItems, "Text", "Value");
            return View(disk);
        }

        // POST: Disk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Edit([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,DateUpdate,DateCreate")] Disk disk)
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "RENTABLE",
                Value = "RENTABLE"
            });
            listItems.Add(new SelectListItem
            {
                Text = "BOOKED",
                Value = "BOOKED"
            });
            listItems.Add(new SelectListItem
            {
                Text = "RENTED",
                Value = "RENTED"
            });
            ViewBag.ListStatus = new SelectList(listItems, "Text", "Value");

            bool flag = true;
            if (flag)
            {
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                disk.UpdatedUser = Int32.Parse(userSession.UserID);
                disk.DateUpdate = DateTime.Now;

                // Check PurchasePrice
                if (!CheckPurchasePrice(disk.PurchasePrice))
                {
                    ViewBag.purchasePrice = "Purchase Price is not valid";
                    return View(disk);
                }
                else
                {
                    ViewBag.purchasePrice = "";
                }

                db.UpdateDisk(disk);
                ViewBag.ok = "Cập nhật thành công";
                return View("Success");
            }
            ViewBag.ok = "Cập nhật không thành công";
            return View("Failure");
        }

        // GET: Disk/Delete
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Delete(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // POST: Disk/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult DeleteConfirmed(int id)
        {
            Disk disk = db.GetDiskById(id);
            db.DeleteDisk(disk);
            ViewBag.ok = "Xóa thành công";
            return View("Success");
        }

        // Check Purchase Price
        private bool CheckPurchasePrice(float num)
        {
            if (num <= 0)
                return false;
            else
                return true;
        }

        // Check Number
        private bool CheckNumber(int num)
        {
            if (num <= 0)
                return false;
            else
                return true;
        }
    }
}
