using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DiskPriceView
    {
        public DiskPriceView(string diskID, string diskTitleName, float price)
        {
            this.diskID = diskID;
            this.diskTitleName = diskTitleName;
            this.price = price;
        }

        public string diskID { set; get; }
        public string diskTitleName { set; get; }
        public float price { set; get; }
    }
}