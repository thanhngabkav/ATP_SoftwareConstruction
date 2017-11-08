﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class CustomerModel
    {
        public CustomerModel()
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
        public int UpdatedUser { get; set; }

        [ForeignKey("UpdatedUser")]
        public virtual User Updated_User { set; get; }

        public virtual ICollection<TransactionHistory> TransactionHistorys { set; get; }

        public virtual ICollection<Reservation> Reservations { set; get; }
    }
}