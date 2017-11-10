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
    public class RentalRatesController : Controller
    {
        private IRentalRate db;
        private IDiskTitleService dbDiskTitle;

        public RentalRatesController(IRentalRate rentalRateService, IDiskTitleService dbDiskTitle)
        {
            this.db = rentalRateService;
            this.dbDiskTitle = dbDiskTitle;
        }

        // GET: RentalRates
        public ActionResult Index()
        {
            var rentalRates = db.GetAllRentalRates();
            return View(rentalRates.ToList());
        }

        // GET: RentalRates/Details/5
        public ActionResult Details(int id)
        {
            RentalRate rentalRate = db.GetCurrentRentalRate(id);
            if (rentalRate == null)
            {
                return HttpNotFound();
            }
            return View(rentalRate);
        }

        // GET: RentalRates/Create
        public ActionResult Create()
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            return View();
        }

        // POST: RentalRates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalRateId,RentalPrice,LateCharge,RentalPeriod,CreatedDate,TitleID")] RentalRate rentalRate)
        {
            if (ModelState.IsValid)
            {
                rentalRate.CreatedDate = DateTime.Now;
                ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
                db.AddNewRentalRate(rentalRate);
                ViewBag.ok = "Thêm thành công";
                return View("Success");
            }

            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", rentalRate.TitleID);
            ViewBag.ok = "Thêm không thành công";
            return View("Failure");
        }
    }
}
