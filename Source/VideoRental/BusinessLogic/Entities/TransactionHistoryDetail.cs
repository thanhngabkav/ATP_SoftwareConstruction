using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{
    public class TransactionHistoryDetail
    {
        [Key]
        public int TransactionDetailID { set; get; }

        [Required]
        public int TransactionID { set; get; }

        [MaxLength(50,ErrorMessage = "Tên Trạng Thái Không Được Vượt Quá 50 Ký Tự")]
        public String Status { set; get; }

        [Required]
        public int DiskID { set; get; }

        public DateTime DateReturn { set; get; }

        public float IncurreCost { set; get; }

        public String Note { set; get; }

        [ForeignKey("DiskID")]
        public virtual Disk Disk { set; get; }

        [ForeignKey("TransactionID")]
        public virtual TransactionHistory TransactionHistory { set; get; }

    }
}
