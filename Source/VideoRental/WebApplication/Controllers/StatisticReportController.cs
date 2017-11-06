using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using WebApplication.Models;
namespace WebApplication.Controllers
{
    public class StatisticReportController : Controller
    {
        IStatisticReportService statisticReportService;
        // GET: StatisticReport
        public ActionResult Report_Title()
        {
            List<TitleReportModel> listResult = statisticReportService.Report_Title();
            return View(listResult);
        }
    }
}