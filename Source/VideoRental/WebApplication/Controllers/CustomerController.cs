using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.DBContext;
using DataAccess.Entities;
using WebApplication.Services;
using WebApplication.Models;
using DataAccess.Utilities;
using System.Text.RegularExpressions;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService db;

        public CustomerController(ICustomerService customerService)
        {
            this.db = customerService;
        }

        // GET: Customer
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Index()
        {
            var customers = db.GetAllCustomer();
            return View(customers.ToList());
        }

        // GET: Customer/Details
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Details(int id)
        {
            Customer customer = db.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Address,PhoneNumber,DateOfBirth,DateCreate,DateUpdate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                customer.UpdatedUser = Int32.Parse(userSession.UserID);

                //check firstName
                if (!CheckFirstName(customer.FirstName))
                {
                    ViewBag.firstName = "First Name is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.firstName = "";
                }

                // check lastName
                if (!CheckLastName(customer.LastName))
                {
                    ViewBag.lastName = "Last Name is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.lastName = "";
                }

                // check date of birth
                if (!CheckDateOfBirth(customer.DateOfBirth))
                {
                    ViewBag.birth = "Date Of Birth is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.birth = "";
                }

                customer.DateCreate = DateTime.Now;
                customer.DateUpdate = DateTime.Now;
                db.AddNewCustomer(customer);
                ViewBag.ok = "Thêm thành công";
                return View("Success");
            }
            return View(customer);
        }

        // GET: Customer/Edit
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Edit(int id)
        {
            Customer customer = db.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Address,PhoneNumber,DateOfBirth,DateCreate,DateUpdate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                UserSession userSession = (UserSession)Session[UserSession.SessionName];
                customer.UpdatedUser = Int32.Parse(userSession.UserID);

                //check firstName
                if (!CheckFirstName(customer.FirstName))
                {
                    ViewBag.firstName = "First Name is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.firstName = "";
                }

                // check lastName
                if (!CheckLastName(customer.LastName))
                {
                    ViewBag.lastName = "Last Name is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.lastName = "";
                }

                // check date of birth
                if (!CheckDateOfBirth(customer.DateOfBirth))
                {
                    ViewBag.birth = "Date Of Birth is not valid";
                    return View(customer);
                }
                else
                {
                    ViewBag.birth = "";
                }

                customer.DateUpdate = DateTime.Now;
                db.UpdateCustomer(customer);
                ViewBag.ok = "Cập nhật thành công";
                return View("Success");
            }
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", customer.UpdatedUser);
            return View(customer);
        }

        // GET: Customer/Delete
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Delete(int id)
        {
            Customer customer = db.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.GetCustomerById(id);
            db.DeleteCustomer(customer);
            ViewBag.ok = "Xóa thành công";
            return View("Success");
        }

        // Check FirstName
        private bool CheckFirstName(string firstName)
        {
            bool match = true;
            match = Regex.IsMatch(firstName, "[^a-zA-Z ]");
            ViewBag.firstName = firstName;
            ViewBag.match = match;
            if (match)
                return false;
            else
                return true;
        }

        // Check FirstName
        private bool CheckLastName(string lastName)
        {
            bool match = true;
            match = Regex.IsMatch(lastName, "[^a-zA-Z ]");
            if (match)
                return false;
            else
                return true;
        }

        // Check Date Of Birth
        private bool CheckDateOfBirth(DateTime dateTime)
        {
            DateTime now = DateTime.Now;
            int now_day = now.Day;
            int now_month = now.Month;
            int now_year = now.Year;

            // Request
            int sam_day = dateTime.Day;
            int sam_month = dateTime.Month;
            int sam_year = dateTime.Year;

            if (sam_year > now_year)
                return false;
            if (sam_year == now_year && sam_month > now_month)
                return false;
            if (sam_year == now_year && sam_month == now_month && sam_day >= now_day)
                return false;
            return true;
        }
    }
}
