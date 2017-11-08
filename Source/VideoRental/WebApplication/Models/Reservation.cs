using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Reservation
    {
        [Key]
        [Column(Order = 1)]
        public int TitleID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int CustomerID { set; get; }

        public DateTime ReservationDate { set; get; }

        [MaxLength(50, ErrorMessage = "Tên Trạng Thái Không Quá 50 Ký Tự")]
        public String Status { set; get; }

        [ForeignKey("TitleID")]
        public virtual DiskTitleModel DiskTitle { set; get; }

        [ForeignKey("CustomerID")]
        public virtual CustomerModel Customer { set; get; }
    }
}