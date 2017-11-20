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

        [Display(Name ="Mã Khách Hàng")]
        public int customerID { set; get; }
        [Display(Name = "Tên")]
        public string customerFirstName { set; get; }
        [Display(Name = "Họ")]
        public string customerLastName { set; get; }
        [Display(Name = "Địa Chỉ")]
        public string address { set; get; } 
    }
}