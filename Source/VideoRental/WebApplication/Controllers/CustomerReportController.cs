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
        // GET: All Customer Report
        public ActionResult Report_AllCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult =  customerReportService.Report_AllCustomer();
            return View(listResult);
        }

        public ActionResult Report_OverDueCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult = customerReportService.Report_OverDueCustomer();
            return View(listResult);
        }

        public ActionResult Report_LateFeeCustomer()
        {
            List<CustomerReportModel> listResult = new List<CustomerReportModel>();
            listResult = customerReportService.Report_LateFeeCustomer();
            return View(listResult);
        }
    }
}