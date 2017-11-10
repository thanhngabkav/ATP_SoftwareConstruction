using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class CustomerReportController : Controller
    {
        ICustomerReportService customerReportService;

        public CustomerReportController(ICustomerReportService customerReportService)
        {
            this.customerReportService = customerReportService;
        }

        // GET: All Customer Report
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Report_AllCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult =  customerReportService.Report_AllCustomer();
            return View(listResult);
        }
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Report_OverDueCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult = customerReportService.Report_OverDueCustomer();
            return View(listResult);
        }
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Report_LateFeeCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult = customerReportService.Report_LateFeeCustomer();
            return View(listResult);
        }
    }
}