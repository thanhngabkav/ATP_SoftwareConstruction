using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class TransactionHistoryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionDetailID { set; get; }

        [Required]
        public int TransactionID { set; get; }

        [MaxLength(50, ErrorMessage = "Tên Trạng Thái Không Được Vượt Quá 50 Ký Tự")]
        public String Status { set; get; }

        [Required]
        public int DiskID { set; get; }

        public DateTime? DateReturn { set; get; }

        public float IncurreCost { set; get; }

        public String Note { set; get; }

        [ForeignKey("DiskID")]
        public virtual DiskModel Disk { set; get; }

        [ForeignKey("TransactionID")]
        public virtual TransactionHistory TransactionHistory { set; get; }
    }
}