using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TitleInfoModel
    {
        public int TitleID { set; get; }

        public String Title { set; get; }

        public int Quantity { set; get; }

        //public float RentalPrice { set; get; }

        //public float LateChargePerDate { set; get; }

        public int NumberOfDiskRentable { set; get; }
    }
}