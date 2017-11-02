using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
using WebApplication.Models;
using DataAccess.Utilities;

namespace WebApplication.Services
{
    public class LateChargesService : ILateChargesServices
    {

        TranSactionDAO transactionDao;
        TransactionDetailsDAO transactionDetailsDao;
        CustomerDAO customerDao;

        public LateChargesService()
        {
            this.transactionDao = new TranSactionDAO();
            this.transactionDetailsDao = new TransactionDetailsDAO();
            this.customerDao = new CustomerDAO();
        }

        public void AddLateCharge(string transactionHistoryDetailID)
        {
            TagDebug.D(GetType(), "in AddLateCharge class");
            TransactionHistoryDetail transaction = transactionDetailsDao.GetTransactionDetail(Int32.Parse(transactionHistoryDetailID));
            transaction.Status = TransactionStatus.DUE;
            transactionDetailsDao.UpdateTransactionDetail(transaction);
        }


        public void CancelLateCharge(string customerID, int numberCancel)
        {
            TagDebug.D(GetType(), "in CancelLateCharge class");
            IList<TransactionHistory> transactionHistory = transactionDao.GetCustomerLateChargeTransactions(Int32.Parse(customerID));

            // cancel top n number
            for (int i = 0; i < numberCancel; ++i)
            {
                transactionHistory[i].Status = TransactionStatus.CANCELED;
                UpdateCancelTransactionDetail(transactionHistory[i]);
            }
        }

        private void UpdateCancelTransactionDetail(TransactionHistory transactionHistory)
        {
            IList<TransactionHistoryDetail> details = transactionDetailsDao.GetListTransactionDetailsByTransactionId(transactionHistory.TransactionHistoryID);
            foreach(TransactionHistoryDetail aDetail in details)
            {
                aDetail.Status = TransactionStatus.PAID;
                transactionDetailsDao.UpdateTransactionDetail(aDetail);
            }

        }

        public IList<CustomerView> FindCustomersHasLateCharge()
        {
            TagDebug.D(GetType(), "in FindCustomersHasLateCharge class");
            IList<Customer> customers = customerDao.GetListLateFeeCustomers();
            IList<CustomerView> customerViews = new List<CustomerView>();
            foreach (Customer c in customers)
            {
                customerViews.Add(new CustomerView(c.CustomerID, c.FirstName, c.LastName, c.Address));
            }
            return customerViews;
        }

        public IList<TransactionHistory> GetAllLateChargeOfCustomer(string customerID)
        {
            TagDebug.D(GetType(), "in GetAllLateChargeOfCustomer class");
            throw new NotImplementedException();
        }

        public void RecordLateCharge(string customerId, int numberLateCharges)
        {
            TagDebug.D(GetType(), "in RecordLateCharge class");
            // need find TransactionDetail by transactionHistoryDetailID
            IList<TransactionHistory> transactionHistories = transactionDao.GetCustomerLateChargeTransactions(Int32.Parse(customerId));

            int numb = numberLateCharges;
            foreach (TransactionHistory hi in transactionHistories)
            {
                List<TransactionHistoryDetail> transactionHistoryDetails = transactionDetailsDao.GetListTransactionDetailsByTransactionId(hi.TransactionHistoryID);
                foreach (TransactionHistoryDetail de in transactionHistoryDetails)
                {
                    de.Status = TransactionStatus.PAID;
                    transactionDetailsDao.UpdateTransactionDetail(de);
                    if (--numb <= 0)
                        break;
                }
                hi.Status = TransactionStatus.PAID;
                transactionDao.UpdateTransaction(hi);
                if (--numb <= 0)
                    break;

            }
        }

        public IList<TransactionHistory> ShowLateCharge(string customerID)
        {
            // not use
            throw new NotImplementedException();
        }
    }
}