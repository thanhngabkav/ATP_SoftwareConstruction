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
    public class Customer
    {
        public Customer()
        {
            this.TransactionHistorys = new List<TransactionHistory>();
            this.Reservations = new List<Reservation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { set; get; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        [Phone(ErrorMessage = "Số Điện Thoại Không Hợp Lệ")]
        public String PhoneNumber { get; set; }

        public DateTime DateOfBirth { set; get; }

        [Required]
        public DateTime DateCreate { set; get; }

        [Required]
        public DateTime DateUpdate { set; get; }

        [Required]
        public int CreatedUser { get; set; }

        [Required]
        public int UpdatedUser { get; set; }


        [ForeignKey("CreatedUser")]
        public virtual User Created_User { set; get; }

        [ForeignKey("UpdatedUser")]
        public virtual User Updated_User { set; get; }

        public virtual ICollection<TransactionHistory> TransactionHistorys { set; get; }

        public virtual ICollection<Reservation> Reservations { set; get; }

    }
}
