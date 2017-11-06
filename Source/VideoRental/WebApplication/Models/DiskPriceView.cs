using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DiskPriceView
    {
        public DiskPriceView(int diskID, string diskTitleName, float price)
        {
            this.diskID = diskID;
            this.diskTitleName = diskTitleName;
            this.price = price;
        }
        [Display(Name = "Disk ID")]
        public int diskID { set; get; }
        [Display(Name = "Title")]
        public string diskTitleName { set; get; }
        [Display(Name = "Price")]
        public float price { set; get; }
    }
}