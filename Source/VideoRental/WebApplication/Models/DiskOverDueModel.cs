using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DiskOverDueModel
    {
        public int DiskID { get; set; }
        public string TitleName { get; set; }
        public DateTime DateReturn { set; get; }
    }
}