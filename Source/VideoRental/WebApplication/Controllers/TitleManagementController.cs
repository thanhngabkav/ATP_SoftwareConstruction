using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;
namespace WebApplication.Controllers
{
    public class TitleManagementController : Controller
    {
        ITitleManagementService titleManagementService;

        public TitleManagementController(ITitleManagementService titleManagementService)
        {
            this.titleManagementService = titleManagementService;
        }
        // GET: TitleManagement
        public ActionResult GetTitleInfomation(int id)
        {
            TitleInfoModel titleModel = titleManagementService.GetInfomationTitle(id);
            return View(titleModel);
        }
    }
}