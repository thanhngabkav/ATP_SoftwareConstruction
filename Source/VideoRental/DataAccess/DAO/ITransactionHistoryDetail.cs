using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
namespace DataAccess.DAO
{
    public interface ITransactionHistoryDetail
    {
        TransactionHistoryDetail getTransactionHistoryDetail(String transactionDetailID);

        void writeTransactionHistoryDetail(Customer customerID, Disk[] disks);

        void writeTransactionHistoryDetail(TransactionHistoryDetail transactionHistoryDetail);

        void writeStatusTransactionHistoryDetail(String transactionDetailID, String status);
    }
}
