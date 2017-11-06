using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    public class TransactionHistory
    {
        public TransactionHistory()
        {
            this.TransactionHistoryDetails = new List<TransactionHistoryDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionHistoryID { get; set; }

        [Required]
        public DateTime CreatedDate { set; get; }
        [Required]
        public float TotalPurchaseCost { get; set; }

        [MaxLength(50, ErrorMessage = "Tên Trạng Thái Không Quá 50 Ký Tự")]
        public String Status { get; set; }

        public String Note { get; set; }

        public int ClerkID { set; get; }

        public int CustomerID { set; get; }

        [ForeignKey("ClerkID")]
        public virtual User Clerk { set; get; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<TransactionHistoryDetail> TransactionHistoryDetails { set; get; }

    }
}
