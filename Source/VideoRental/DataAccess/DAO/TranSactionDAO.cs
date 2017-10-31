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
    /// <summary>
    /// Transaction Data Access
    /// </summary>
    public class TranSactionDAO
    {
        private VideoRentalDBContext dBContext;

        public TranSactionDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Add new Transaction into database (not include transaction details)
        /// </summary>
        /// <param name="transaction"></param>
        public void AddnewTransaction(TransactionHistory transaction)
        {
            dBContext.TransactionHistories.Add(transaction);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Remove Transaction history from database (include transaction details)
        /// </summary>
        /// <param name="transaction"></param>
        public void DeleteTransaction(TransactionHistory transaction)
        {
            dBContext.TransactionHistories.Remove(transaction);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Update transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void UpdateTransaction(TransactionHistory transaction)
        {
            dBContext.Entry(transaction).State = EntityState.Modified;
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get list customer's transactions by customer's id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<TransactionHistory> GetAllCustomerTransactions(int customerId)
        {
            return dBContext.TransactionHistories.Where(x => x.CustomerID == customerId).ToList();
        }


        /// <summary>
        /// Get list Late charge transactions of customer by customer's id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<TransactionHistory> GetCustomerLateChargeTransactions(int customerId)
        {
            return dBContext.TransactionHistories.Where(x => x.CustomerID == customerId && x.Status.Equals(TransactionStatus.DUE)).ToList();
        }


        /// <summary>
        /// Get TransactionHistory by transaction history id
        /// </summary>
        /// <param name="transcationID"></param>
        /// <returns></returns>
        public TransactionHistory GetTransaction(int transcationID)
        {
            return dBContext.TransactionHistories.Where(x => x.TransactionHistoryID == transcationID).SingleOrDefault();
        }
    }
}
