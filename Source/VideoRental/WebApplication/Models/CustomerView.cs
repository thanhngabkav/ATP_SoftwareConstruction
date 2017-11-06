using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name ="Customer ID")]
        public int customerID { set; get; }
        [Display(Name = "First Name")]
        public string customerFirstName { set; get; }
        [Display(Name = "Last Name")]
        public string customerLastName { set; get; }
        [Display(Name = "Address")]
        public string address { set; get; } 
    }
}