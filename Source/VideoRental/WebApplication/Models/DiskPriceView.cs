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
        [Display(Name = "Mã Đĩa")]
        public int diskID { set; get; }
        [Display(Name = "Tiều Đề")]
        public string diskTitleName { set; get; }
        [Display(Name = "Giá")]
        public float price { set; get; }
    }
}