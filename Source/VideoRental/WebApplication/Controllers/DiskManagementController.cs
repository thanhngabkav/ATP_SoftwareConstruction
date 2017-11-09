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
    public class DiskManagementController : Controller
    {
        IDiskManagementService diskManagementService;

        public DiskManagementController(IDiskManagementService diskManagementService)
        {
            this.diskManagementService = diskManagementService;
        }
        // GET: DiskManagement
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = UserRole.Clerk)]
        public ActionResult GetDiskStatus(int Id)
        {
            DiskStatusInfoModel result = diskManagementService.GetDiskStatus(Id);
            return View(result);
        }
    }
}