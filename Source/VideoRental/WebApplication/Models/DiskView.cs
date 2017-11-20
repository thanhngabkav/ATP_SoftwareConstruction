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
            IsChosen = false;
        }

        [Display(Name ="Mã Đĩa")]
        public int DiskID { set; get; }
        [Display(Name = "Tiêu Đề")]
        public string DiskTitleName { set; get; }

        [Display(Name = "Giá Thuê Đĩa")]
        public float Prices { set; get; } // PurchasePrice

        public string ImageUrl { set; get; }
        [Display(Name = "Trạng Thái")]
        public string Status { set; get; }
        public bool IsChosen { set; get; }
    }
}