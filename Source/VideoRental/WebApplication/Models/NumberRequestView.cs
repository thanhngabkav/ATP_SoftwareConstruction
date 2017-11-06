using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class NumberRequestView
    {
        [Display(Name = "Số lượng trễ hạn muốn thanh toán")]
        public int number { set; get; }
    }
}