﻿using System;
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
        public ActionResult Create([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,DateUpdate,DateCreate")] Disk disk)
        {
            bool flag = true;
            if (flag)
            {
                flag = false;
                ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
                disk.Status = "RENTABLE";
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                disk.UpdatedUser = Int32.Parse(userSession.UserID);
                disk.DateCreate = DateTime.Now;
                disk.DateUpdate = DateTime.Now;
                disk.RentedTime = 0;
                disk.LastRentedDate = null;

                db.AddNewDisk(disk);
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
            bool flag = true;
            if (flag)
            {
                ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                disk.UpdatedUser = Int32.Parse(userSession.UserID);
                disk.DateUpdate = DateTime.Now;
                String status = disk.Status;
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
    }
}
