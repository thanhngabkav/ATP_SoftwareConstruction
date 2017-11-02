using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.DBContext;
using System.Data.Entity;
using DataAccess.Utilities;

namespace DataAccess.DAO
{
    public class TransactionDetailsDAO
    {
        private VideoRentalDBContext dBContext;

        public TransactionDetailsDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Get List transaction details by transaction id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public List<TransactionHistoryDetail> GetListTransactionDetailsByTransactionId(int transactionId)
        {
            return dBContext.TransactionHistoryDetails.Where(x => x.TransactionID == transactionId).ToList();
        }

        /// <summary>
        /// Get TransactionHistoryDetail by transaction details id
        /// </summary>
        /// <param name="transcationDetailID"></param>
        /// <returns></returns>
        public TransactionHistoryDetail GetTransactionDetail(int transcationDetailID)
        {
            return dBContext.TransactionHistoryDetails.Where(x => x.TransactionDetailID == transcationDetailID).SingleOrDefault();
        }

        /// <summary>
        /// Update Transaction Detail
        /// </summary>
        /// <param name="detail"></param>
        public void UpdateTransactionDetail(TransactionHistoryDetail detail)
        {
            dBContext.Entry(detail).State = EntityState.Modified;
            dBContext.SaveChanges();
        }


        /// <summary>
        /// Add Transaction Detail
        /// </summary>
        /// <param name="detail"></param>
        public void AddTransactionDetail(TransactionHistoryDetail detail)
        {
            dBContext.Entry(detail).State = EntityState.Added;
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get the transaction detail of last retrun date
        /// </summary>
        /// <param name="diskID"></param>
        /// <returns></returns>
        public TransactionHistoryDetail getTransactionDetailFromLastRentedDate(int diskID)
        {
            List<TransactionHistoryDetail> listDiskTransactionDetails = new List<TransactionHistoryDetail>();
            listDiskTransactionDetails = dBContext.TransactionHistoryDetails.Where(x => x.DiskID == diskID).ToList();
            List<TransactionHistory> listTransactionHistory = new List<TransactionHistory>();
            foreach(TransactionHistoryDetail transactionDetail in listDiskTransactionDetails)
            {
                listTransactionHistory.Add(dBContext.TransactionHistories.Where(x => x.TransactionHistoryID == transactionDetail.TransactionID).SingleOrDefault());
            }
            TransactionHistory transactionHistory = listTransactionHistory[0];
            foreach(TransactionHistory transaction in listTransactionHistory)
            {
                if(transaction.CreatedDate > transactionHistory.CreatedDate)
                {
                    transactionHistory = transaction;
                }
            }
            return dBContext.TransactionHistoryDetails.Where(x => x.TransactionID == transactionHistory.TransactionHistoryID &&
                                                                x.DiskID == diskID).SingleOrDefault();
        }
    }
}
