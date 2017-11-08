using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class DiskTitleModel
    {
        public DiskTitleModel()
        {
            this.Disks = new List<DiskModel>();
            this.Reservations = new List<ReservationModel>();
            this.RentalRates = new List<RentalRateModel>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TitleID { set; get; }

        [Required]
        public String Title { set; get; }

        public String Tags { set; get; }

        public String ImageLink { set; get; }

        public int Quantity { set; get; }

        public virtual ICollection<DiskModel> Disks { set; get; }

        public virtual ICollection<ReservationModel> Reservations { set; get; }

        public virtual ICollection<RentalRateModel> RentalRates { set; get; }
    }
}