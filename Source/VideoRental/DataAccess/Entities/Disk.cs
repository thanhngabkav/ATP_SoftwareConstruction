using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities
{
    public class Disk
    {
        public Disk()
        {
            this.TransactionHistoryDetails = new List<TransactionHistoryDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiskID { get; set; }

        [Required]
        public int TitleID { get; set; }

        [Required]
        public String Status { get; set; }
        [Required]
        public float PurchasePrice { set; get; }

        public int RentedTime { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastRentedDate { set; get; }

        public DateTime DateUpdate { get; set; }

        public DateTime DateCreate { set; get; }

        public int CreatedUser { set; get; }

        [ForeignKey("CreatedUser")]
        public virtual User Created_User { set; get; }

        [ForeignKey("TitleID")]
        public virtual DiskTitle DiskTitle { set; get; }

        public virtual ICollection<TransactionHistoryDetail> TransactionHistoryDetails { set; get; }
    }
}
