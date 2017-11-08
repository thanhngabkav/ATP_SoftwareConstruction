using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class RentalRateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalRateId { set; get; }

        public float RentalPrice { set; get; }

        public float LateCharge { set; get; }

        public int RentalPeriod { set; get; }

        public DateTime CreatedDate { set; get; }

        public int TitleID { set; get; }

        [ForeignKey("TitleID")]
        public virtual DiskTitleModel DiskTitle { set; get; }
    }
}