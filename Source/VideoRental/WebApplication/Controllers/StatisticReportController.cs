using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using WebApplication.Models;
using DataAccess.Utilities;
namespace WebApplication.Controllers
{
    public class StatisticReportController : Controller
    {
        IStatisticReportService statisticReportService;

        public StatisticReportController(IStatisticReportService statisticReportService)
        {
            this.statisticReportService = statisticReportService;
        }
        // GET: StatisticReport
        [Authorize(Roles = UserRole.Manager)]
        public ActionResult Report_Title()
        {
            List<TitleReportModel> listResult = statisticReportService.Report_Title();
            return View(listResult);
        }
    }
}