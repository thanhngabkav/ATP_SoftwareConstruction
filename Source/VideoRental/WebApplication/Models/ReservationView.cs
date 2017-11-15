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
        [Display(Name ="Title ID")]
        public int titleID { set; get; }
        [Display(Name = "Title Name")]
        public string diskNameTitle { set; get; }
        [Display(Name = "Customer ID")]
        public int customerID { set; get; }
        [Display(Name = "Customer Name")]
        public string customerName { set; get; }
        [Display(Name ="Date Reservation")]
        public DateTime dateReservate { set; get; }
        [Display(Name = "Status")]
        public string Status { set; get; }

    }
}