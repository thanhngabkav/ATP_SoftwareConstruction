using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    /// <summary>
    /// Mô tả thông tin về các khoản tiền phạt
    /// </summary>
    public class LateCharge
    {
        public int DiskID { get; set; }
        public string Title { get; set; }
        //Ngày phải trả
        public DateTime DateReturn { set; get; }
        //Ngày trả thực
        public DateTime? DateActuallyReturn { set; get; }
        public float Cost { set; get; }
    }
}