using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
namespace WebApplication.Services
{
    /// <summary>
    /// Customer report service
    /// </summary>
    public class CustomerReportService : ICustomerReportService
    {
        public List<Customer> Report_AllCustomer()
        {
            throw new NotImplementedException();
        }

        public List<Customer> Report_LateFeeCustomer()
        {
            throw new NotImplementedException();
        }

        public List<Customer> Report_OverDueCustomer()
        {
            throw new NotImplementedException();
        }
    }
}