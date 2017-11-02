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
    public class CustomerController : Controller
    {
        private CustomerService db;

        public CustomerController()
        {
            db = new CustomerService();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.GetAllCustomer();
            return View(customers.ToList());
        }

        // GET: Customer/Details
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
        public ActionResult Create()
        {
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName");
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Address,PhoneNumber,DateOfBirth,DateCreate,DateUpdate,CreatedUser,UpdatedUser")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.AddNewCustomer(customer);
                return RedirectToAction("Index");
            }

            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", customer.CreatedUser);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", customer.UpdatedUser);
            return View(customer);
        }

        // GET: Customer/Edit
        public ActionResult Edit(int id)
        {
            Customer customer = db.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", customer.CreatedUser);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", customer.UpdatedUser);
            return View(customer);
        }

        // POST: Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Address,PhoneNumber,DateOfBirth,DateCreate,DateUpdate,CreatedUser,UpdatedUser")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            //ViewBag.CreatedUser = new SelectList(db.Users, "UserID", "UserName", customer.CreatedUser);
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", customer.UpdatedUser);
            return View(customer);
        }

        // GET: Customer/Delete
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
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.GetCustomerById(id);
            db.DeleteCustomer(customer);
            return RedirectToAction("Index");
        }
    }
}
