using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Entities;

namespace DataAccess.Entities
{
    public class User
    {
        public User()
        {
            this.CreatedCustomers = new List<Customer>();
            this.UpdatedCustomers = new List<Customer>();
            this.TransactionHistorys = new List<TransactionHistory>();
            this.Disks = new List<Disk>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { set; get; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tên Đăng Nhập Không Vượt Quá 200 Ký Tự")]
        public String UserName { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Mật Khẩu Không Vượt Quá 200 Ký Tự")]
        public String Password { set; get; }

        [Required]
        public String Address { get; set; }

        [Required]
        [Phone(ErrorMessage = "Số Điện Thoại Không Hợp Lệ")]
        public String PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Email Không Hợp Lệ")]
        public String Email { get; set; }

        [MaxLength(50, ErrorMessage = "Tên Quyền Không Quá 50 Ký Tự")]
        public String Role { set; get; }

        public virtual ICollection<Customer> CreatedCustomers { set; get; }

        public virtual ICollection<Customer> UpdatedCustomers { set; get; }

        public virtual ICollection<TransactionHistory> TransactionHistorys { set; get; }

        public virtual ICollection<Disk> Disks { set; get; }
    }
}