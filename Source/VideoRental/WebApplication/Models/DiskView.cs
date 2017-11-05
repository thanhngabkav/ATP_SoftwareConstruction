using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WebApplication.Models
{
    public class DiskView
    {
        public DiskView(int diskID, string diskTitleName, float prices, string imageUrl, string status)
        {
            this.DiskID = diskID;
            this.DiskTitleName = diskTitleName;
            this.Prices = prices;
            this.ImageUrl = imageUrl;
            this.Status = status;
        }

        [Display(Name ="Disk ID")]
        public int DiskID { set; get; }
        [Display(Name = "Title")]
        public string DiskTitleName { set; get; }


        public float Prices { set; get; } // PurchasePrice

        public string ImageUrl { set; get; }

        public string Status { set; get; }
    }
}