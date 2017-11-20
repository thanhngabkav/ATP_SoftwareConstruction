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
using DataAccess.Utilities;

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
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Index()
        {
            var rentalRates = db.GetAllRentalRates();
            return View(rentalRates.ToList());
        }

        // GET: RentalRates/Details/5
        [Authorize(Roles = UserRole.Manager)]
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
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Create()
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            return View();
        }

        // POST: RentalRates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Create([Bind(Include = "RentalRateId,RentalPrice,LateCharge,RentalPeriod,CreatedDate,TitleID")] RentalRate rentalRate)
        {
            ViewBag.TitleID = new SelectList(dbDiskTitle.GetAllTitles(), "TitleID", "Title");
            if (ModelState.IsValid)
            {
                rentalRate.CreatedDate = DateTime.Now;
                // Check Rental Price
                if (rentalRate.RentalPrice < 0.0)
                {
                    ViewBag.rentalPrice = "Rental Rate is not valid";
                    return View(rentalRate);
                }
                else
                {
                    ViewBag.rentalPrice = "";
                }

                // Check Late Charge
                if (rentalRate.LateCharge < 0.0)
                {
                    ViewBag.lateCharge = "Late Charge is not valid";
                    return View(rentalRate);
                }
                else
                {
                    ViewBag.lateCharge = "";
                }

                // Check Rental Period
                if (rentalRate.RentalPeriod < 1)
                {
                    ViewBag.rentalPeriod = "Rental Period is not valid";
                    return View(rentalRate);
                }
                else
                {
                    ViewBag.rentalPeriod = "";
                }

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
