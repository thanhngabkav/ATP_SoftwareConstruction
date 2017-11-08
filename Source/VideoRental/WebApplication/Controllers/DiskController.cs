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

namespace WebApplication.Controllers
{
    public class DiskController : Controller
    {
        IDiskManagementService diskManagement;
        IDiskService db;

        public DiskController(IDiskManagementService diskManagement, IDiskService diskService)
        {
            this.diskManagement = diskManagement;
            this.db = diskService;
        }

        // GET: Disks
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
        public ActionResult Create()
        {
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title");
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Disk/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,DateUpdate,DateCreate,UpdatedUser")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.AddNewDisk(disk);
                return RedirectToAction("Index");
            }

            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", disk.UpdatedUser);
            return View(disk);
        }

        // GET: Disk/Edit
        public ActionResult Edit(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", disk.UpdatedUser);
            return View(disk);
        }

        // POST: Disk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,DateUpdate,DateCreate,UpdatedUser")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.UpdateDisk(disk);
                return RedirectToAction("Index");
            }
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", disk.UpdatedUser);
            return View(disk);
        }

        // GET: Disk/Delete
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
        public ActionResult DeleteConfirmed(int id)
        {
            Disk disk = db.GetDiskById(id);
            db.DeleteDisk(disk);
            return RedirectToAction("Index");
        }

        public ActionResult GetDiskStatus(int diskId)
        {
            return View(diskManagement.GetDiskStatus(diskId));
        }
    }
}
