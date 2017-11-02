using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DiskView
    {
        public string diskID { set; get; }
        public string diskTitleName { set; get; }

        public string prices { set; get; } // PurchasePrice

        public string imageUrl { set; get; }

        public string status { set; get; }
    }
}