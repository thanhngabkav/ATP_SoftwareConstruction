using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CustomerView
    {
        public CustomerView(int customerID, string customerFirstName, string customerLastName, string address)
        {
            this.customerID = customerID;
            this.customerFirstName = customerFirstName;
            this.customerLastName = customerLastName;
            this.address = address;
        }

        public int customerID { set; get; }
        public string customerFirstName { set; get; }
        public string customerLastName { set; get; }
        public string address { set; get; } 
    }
}