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
    public class DiskTitleController : Controller
    {
        private DiskTitleService db;

        public DiskTitleController()
        {
            this.db = new DiskTitleService();
        }

        // GET: DiskTitle
        public ActionResult Index()
        {
            return View(db.GetAllTitles().ToList());
        }

        // GET: DiskTitle/Details
        public ActionResult Details(int id)
        {
            DiskTitle diskTitle = db.GetTitleById(id);
            if (diskTitle == null)
            {
                return HttpNotFound();
            }
            return View(diskTitle);
        }

        // GET: DiskTitle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiskTitle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitleID,Title,Tags,Image,Quantity,RentalPrice,LateChargePerDate")] DiskTitle diskTitle)
        {
            if (ModelState.IsValid)
            {
                db.AddNewTitle(diskTitle);
                return RedirectToAction("Index");
            }
            return View(diskTitle);
        }

        // GET: DiskTitle/Delete
        public ActionResult Delete(int id)
        {
            DiskTitle diskTitle = db.GetTitleById(id);
            if (diskTitle == null)
            {
                return HttpNotFound();
            }
            return View(diskTitle);
        }

        // POST: DiskTitle/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiskTitle diskTitle = db.GetTitleById(id);
            db.DeleteTitle(diskTitle);
            return RedirectToAction("Index");
        }
    }
}
