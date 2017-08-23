using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{
    public class RentalRate
    {
        public RentalRate()
        {
            this.DiskTitles = new List<DiskTitle>();
        }

        [Key]
        public int RentalRateID { get; set; }

        [Required]
        public String Name { set; get; }

        [Required]
        public float RentalPrice { set; get; }

        [Required]
        public float LatePricePerDate { set; get; }

        public virtual ICollection<DiskTitle> DiskTitles { set; get; }
    }
}
