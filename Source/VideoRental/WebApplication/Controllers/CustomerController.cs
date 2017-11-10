﻿using System;
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
                customer.DateCreate = DateTime.Now;
                customer.DateUpdate = DateTime.Now;
                db.AddNewCustomer(customer);
                ViewBag.ok = "Thêm thành công";
                return View("Success");
            }
            ViewBag.ok = "Thêm không thành công";
            return View("Failure");
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
                customer.DateUpdate = DateTime.Now;
                db.UpdateCustomer(customer);
                ViewBag.ok = "Cập nhật thành công";
                return View("Success");
            }
            //ViewBag.UpdatedUser = new SelectList(db.Users, "UserID", "UserName", customer.UpdatedUser);
            ViewBag.ok = "Cập nhật không thành công";
            return View("Failure");
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

       
    }
}
