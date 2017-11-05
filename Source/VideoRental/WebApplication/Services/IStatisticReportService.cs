using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
namespace WebApplication.Services
{
    public interface IStatisticReportService
    {
        List<TitleReportModel> Report_Title();
    }
}