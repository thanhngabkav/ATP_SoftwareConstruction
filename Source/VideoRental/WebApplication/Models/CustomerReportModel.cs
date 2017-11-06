using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
namespace WebApplication.Models
{
    public class CustomerReportModel
    {
        public CustomerReportModel()
        {
            this.LateCharges = new List<LateCharge>();
            this.DiskOverDues = new List<DiskOverDueModel>();
        }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        /// <summary>
        /// Total disk currently has out
        /// </summary>
        public int TotalDisk { set; get; }
        /// <summary>
        /// List disk over due and infomation
        /// </summary>
        public List<DiskOverDueModel> DiskOverDues { set; get; }
        /// <summary>
        /// List fine for each item returned late
        /// </summary>
        public List<LateCharge> LateCharges { set; get; }
        /// <summary>
        /// Total number of fines
        /// </summary>
        public float TotalFines { set; get; }

    }
}