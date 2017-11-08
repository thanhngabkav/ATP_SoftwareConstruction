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

        public RentalRatesController()
        {
            db = new RentalRateService();
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
            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title");
            return View();
        }

        // POST: RentalRates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalRateId,RentalPrice,LateCharge,RentalPeriod,CreatedDate,TitleID")] RentalRate rentalRate)
        {
            if (ModelState.IsValid)
            {
                db.AddNewRentalRate(rentalRate);
                return RedirectToAction("Index");
            }

            //ViewBag.TitleID = new SelectList(db.DiskTitles, "TitleID", "Title", rentalRate.TitleID);
            return View(rentalRate);
        }
    }
}
