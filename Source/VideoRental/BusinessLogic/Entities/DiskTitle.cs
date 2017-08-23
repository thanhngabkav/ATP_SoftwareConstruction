using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{
    public class DiskTitle
    {
        public DiskTitle()
        {
            this.Disks = new List<Disk>();
            this.Reservations = new List<Reservation>();
        }
        [Key]
        public int TitleID { set; get; }
        
        [Required]
        public String Title { set; get; }

        public String Tags { set; get; }

        public byte[] Image { set; get; }

        public int Quantity { set; get; }

        [Required]
        public float RentalPrice { set; get; }

        public float LateChargePerDate { set; get; }

        public virtual ICollection<Disk> Disks { set; get; }

        public virtual ICollection<Reservation> Reservations { set; get; }

    }
}
