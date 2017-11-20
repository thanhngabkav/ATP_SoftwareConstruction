using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ReservationView
    {
        public ReservationView(int titleID, string diskNameTitle, int customerID, string customerName, DateTime dateReservate, string status)
        {
            this.titleID = titleID;
            this.diskNameTitle = diskNameTitle;
            this.customerID = customerID;
            this.customerName = customerName;
            this.dateReservate = dateReservate;
            this.Status = status;
        }
        [Display(Name ="Mã Tiêu Đề")]
        public int titleID { set; get; }
        [Display(Name = "Tên Tiêu Đề")]
        public string diskNameTitle { set; get; }
        [Display(Name = "Mã Khách Hàng")]
        public int customerID { set; get; }
        [Display(Name = "Tên Khách Hàng")]
        public string customerName { set; get; }
        [Display(Name ="Ngày Đặt Trước")]
        public DateTime dateReservate { set; get; }
        [Display(Name = "Trạng Thái")]
        public string Status { set; get; }

    }
}