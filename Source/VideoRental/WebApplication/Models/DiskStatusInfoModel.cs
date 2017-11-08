using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
namespace WebApplication.Models
{
    public class DiskStatusInfoModel
    {
        public int DiskID { set; get; }
        public string Title { set; get; }
        public string Status { set; get; }
        public Customer Whom { set; get; }
        public string CustomerName { set; get; }
        public DateTime? DueTime { set; get; }
    }
}