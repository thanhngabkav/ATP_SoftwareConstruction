using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TransactionHistoryView
    {

        [Display(Name = "Mã Khách Hàng")]
        public int CustomerID { set; get; }
        [Display(Name = "Tên Khách Hàng")]
        public string CustomerName { set; get; }
        [Display(Name = "Tổng Số Trễ Hạn Chưa Trả")]
        public int NumberLateCharge { set; get; }
        public int TransactionHistoryID { get; set; }
        [Display(Name = "Ngày Tạo Giao Dịch")]
        public DateTime CreatedDate { set; get; }
        [Display(Name = "Trạng Thái")]
        public string Status { get; set; }

    }
}