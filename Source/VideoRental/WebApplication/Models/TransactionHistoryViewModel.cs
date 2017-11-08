using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TransactionHistoryViewModel
    {
        public TransactionHistoryViewModel(int transactionHistoryID, DateTime createdDate, string status, int customerID, string customerName)
        {
            TransactionHistoryID = transactionHistoryID;
            CreatedDate = createdDate;
            Status = status;
            CustomerID = customerID;
            CustomerName = customerName;
        }

        public int TransactionHistoryID { get; set; }
        public DateTime CreatedDate { set; get; }
        public string Status { get; set; }
        public int CustomerID { set; get; }
        public string CustomerName { set; get; }
    }
}