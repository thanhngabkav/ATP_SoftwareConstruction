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
        public List<TransactionHistoryDetail> GeListTransactionDetailsByTransactionId(int transactionId)
        {
            return dBContext.TransactionHistoryDetails.Where(x => x.TransactionID == transactionId).ToList();
        }
    }
}
