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
    public class DiskTitleController : Controller
    {
        private IDiskTitleService db;

        public DiskTitleController(IDiskTitleService diskTitleService)
        {
            this.db = diskTitleService;
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
        public ActionResult Create([Bind(Include = "TitleID,Title,Tags,ImageLink,Quantity")] DiskTitle diskTitle)
        {
            if (ModelState.IsValid)
            {
                db.AddNewTitle(diskTitle);
                ViewBag.ok = "Thêm thành công";
                return View("Success");
            }
            ViewBag.ok = "Thêm không thành công";
            return View("Failure");
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
            ViewBag.ok = "Xóa thành công";
            return View("Success");
        }
    }
}
