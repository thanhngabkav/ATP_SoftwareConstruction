using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities
{
    public class RentalRate
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalRaateId { set; get; }

        public float RentalPrice { set; get; }

        public float LateCharge { set; get; }

        public int RentalPeriod { set; get; }

        public DateTime CreatedDate { set; get; }

        public int TitleID { set; get; }

        [ForeignKey("TitleID")]
        public virtual DiskTitle DiskTitle { set; get; }
    }
}
