using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ReservationView
    {
        public ReservationView(int titleID, string diskNameTitle, int customerID, string customerName, DateTime dateReservate)
        {
            this.titleID = titleID;
            this.diskNameTitle = diskNameTitle;
            this.customerID = customerID;
            this.customerName = customerName;
            this.dateReservate = dateReservate;
        }

        public int titleID { set; get; }
        public string diskNameTitle { set; get; }
        public int customerID { set; get; }
        public string customerName { set; get; }
        public DateTime dateReservate { set; get; }

    }
}