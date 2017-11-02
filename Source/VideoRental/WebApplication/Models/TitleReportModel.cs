using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TitleReportModel
    {
        public int TitleID { set; get; }
        public string Title { set; get; }
        public int Total { set; get; }
        //Number of copies currently rented out
        public int NumberOfRentedOut { get; set; }
        //Number currently in stock
        public int NumberOfInStock { get; set; }
        //Number of copies on hold for some customer
        public int NumberOfOnHold { get; set; }

        public int NumberOfReservation { set; get; }
    }
}