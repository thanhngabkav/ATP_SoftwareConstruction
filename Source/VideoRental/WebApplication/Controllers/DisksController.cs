using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.Entities;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class DisksController : Controller
    {
        private DiskService db;

        public DisksController()
        {
            this.db = new DiskService();
        }

        // GET: Disks
        public ActionResult Index()
        {
            var disks = db.GetAllDisks();
            return View(disks.ToList());
        }

        // GET: Disks/Details/5
        public ActionResult Details(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // GET: Disks/Create
        public ActionResult Create()
        {
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName");
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title");
            return View();
        }

        // POST: Disks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,RentalPeriod,DateUpdate,DateCreate,CreatedUser")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.AddNewDisk(disk);
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", disk.CreatedUser);
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            return View(disk);
        }

        // GET: Disks/Edit/5
        public ActionResult Edit(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", disk.CreatedUser);
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            return View(disk);
        }

        // POST: Disks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiskID,TitleID,Status,PurchasePrice,RentedTime,LastRentedDate,RentalPeriod,DateUpdate,DateCreate,CreatedUser")] Disk disk)
        {
            if (ModelState.IsValid)
            {
                db.UpdateDisk(disk);
                return RedirectToAction("Index");
            }
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", disk.CreatedUser);
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", disk.TitleID);
            return View(disk);
        }

        // GET: Disks/Delete/5
        public ActionResult Delete(int id)
        {
            Disk disk = db.GetDiskById(id);
            if (disk == null)
            {
                return HttpNotFound();
            }
            return View(disk);
        }

        // POST: Disks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disk disk = db.GetDiskById(id);
            db.DeleteDisk(disk);
            return RedirectToAction("Index");
        }
    }
}
