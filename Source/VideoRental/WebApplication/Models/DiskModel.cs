using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class DiskModel
    {
        public DiskModel()
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

        public DateTime? LastRentedDate { set; get; }

        public DateTime DateUpdate { get; set; }

        public DateTime DateCreate { set; get; }

        public int UpdatedUser { set; get; }

        [ForeignKey("UpdatedUser")]
        public virtual User Update_User { set; get; }

        [ForeignKey("TitleID")]
        public virtual DiskTitleModel DiskTitle { set; get; }

        public virtual ICollection<TransactionHistoryDetail> TransactionHistoryDetails { set; get; }
    }
}