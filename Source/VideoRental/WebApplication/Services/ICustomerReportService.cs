using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
using WebApplication.Models;
namespace WebApplication.Services
{
    public interface ICustomerReportService
    {
        /// <summary>
        /// All customer report
        /// </summary>
        /// <returns></returns>
        List<CustomerReportModel> Report_AllCustomer();
        /// <summary>
        /// All customer have one or more  overdue item
        /// </summary>
        /// <returns></returns>
        List<CustomerReportModel> Report_OverDueCustomer();
        /// <summary>
        /// All customer have one or more late fee
        /// </summary>
        /// <returns></returns>
        List<CustomerReportModel> Report_LateFeeCustomer();
    }
}