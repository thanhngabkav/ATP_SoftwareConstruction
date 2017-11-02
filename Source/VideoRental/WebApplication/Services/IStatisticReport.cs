using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
namespace WebApplication.Services
{
    public interface IStatisticReport
    {
        List<TitleReportModel> Report_Title();
    }
}