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
        IList<int> diskIDs;
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


        public void CancelLateCharge(int transactionHistoryID)
        {
            TagDebug.D(GetType(), "in CancelLateCharge class");
            TransactionHistory transactionHistory = transactionDao.GetTransaction(transactionHistoryID);
            transactionHistory.Status = TransactionStatus.CANCELED;
            transactionDao.UpdateTransaction(transactionHistory);

            UpdateCancelTransactionDetail(transactionHistory);
        }

        private void UpdateCancelTransactionDetail(TransactionHistory transactionHistory)
        {
            IList<TransactionHistoryDetail> details = transactionDetailsDao.GetListTransactionDetailsByTransactionId(transactionHistory.TransactionHistoryID);
            foreach (TransactionHistoryDetail aDetail in details)
            {
                aDetail.Status = TransactionDetailStatus.PAID;
                transactionDetailsDao.UpdateTransactionDetail(aDetail);
            }

        }

        public IList<CustomerView> FindCustomersHasLateCharge()
        {
            TagDebug.D(GetType(), "in FindCustomersHasLateCharge class");
            IList<Customer> customers = customerDao.GetListLateFeeCustomers();
            IList<CustomerView> customerViews = new List<CustomerView>();
            foreach (Customer c in customers)
                customerViews.Add(new CustomerView(c.CustomerID, c.FirstName, c.LastName, c.Address));
            return customerViews;
        }

        public IList<TransactionHistoryView> GetAllLateChargeOfCustomer(int customerID)
        {
            TagDebug.D(GetType(), "in GetAllLateChargeOfCustomer class");

            IList<TransactionHistory> transactions = transactionDao.GetCustomerLateChargeTransactions(customerID);
            IList<TransactionHistoryView> transactionHistoryViews = new List<TransactionHistoryView>();
            foreach (TransactionHistory a in transactions)
            {
                transactionHistoryViews.Add(new TransactionHistoryView()
                {
                    CreatedDate = a.CreatedDate,
                    CustomerID = a.CustomerID,
                    Status = a.Status,
                    CustomerName = a.Customer.FirstName,
                    NumberLateCharge = GetNumberOfLateCharge(a.CustomerID),
                    TransactionHistoryID = a.TransactionHistoryID
                });
            }

            return transactionHistoryViews;
        }

        public void RecordLateCharge(int customerId, int numberLateCharges)
        {
            TagDebug.D(GetType(), "in RecordLateCharge class");
            // need find TransactionDetail by transactionHistoryDetailID
            IList<TransactionHistory> transactionHistories = transactionDao.GetCustomerLateChargeTransactions(customerId);
            diskIDs = new List<int>();
            int numb = numberLateCharges;
            foreach (TransactionHistory hi in transactionHistories)
            {
                List<TransactionHistoryDetail> transactionHistoryDetails = transactionDetailsDao.GetListTransactionDetailsByTransactionId(hi.TransactionHistoryID);
                foreach (TransactionHistoryDetail de in transactionHistoryDetails)
                {

                    if (de.Status == TransactionDetailStatus.DUE)
                        if (numb-- > 0)
                        {
                            diskIDs.Add(de.DiskID);
                            de.Status = TransactionStatus.PAID;
                            transactionDetailsDao.UpdateTransactionDetail(de);
                        }
                }
                if (numb < 0)
                    break;
                else
                {
                    hi.Status = TransactionStatus.PAID;
                    transactionDao.UpdateTransaction(hi);
                }
            }
        }

        public int GetNumberOfLateCharge(int customerID)
        {
            int number = 0;
            IList<TransactionHistory> transactionHistories = transactionDao.GetCustomerLateChargeTransactions(customerID);
            foreach (TransactionHistory hi in transactionHistories)
            {
                List<TransactionHistoryDetail> transactionHistoryDetails = transactionDetailsDao.GetListTransactionDetailsByTransactionId(hi.TransactionHistoryID);
                foreach (TransactionHistoryDetail de in transactionHistoryDetails)
                {
                    if (de.Status == null) continue;
                    if (de.Status == TransactionDetailStatus.DUE)
                        number++;
                }
            }
            return number;
        }

        public float GetTotalLateChargePrice(int customerId, int numberLateCharges)
        {
            TagDebug.D(GetType(), "in GetTotalLateChargePrice class");

            float totalLateCharge = 0;
            // need find TransactionDetail by transactionHistoryDetailID
            IList<TransactionHistory> transactionHistories = transactionDao.GetCustomerLateChargeTransactions(customerId);
            diskIDs = new List<int>();
            int numb = numberLateCharges;
            foreach (TransactionHistory hi in transactionHistories)
            {
                List<TransactionHistoryDetail> transactionHistoryDetails = transactionDetailsDao.GetListTransactionDetailsByTransactionId(hi.TransactionHistoryID);
                foreach (TransactionHistoryDetail de in transactionHistoryDetails)
                {

                    if (de.Status == TransactionDetailStatus.DUE)
                        if (numb-- > 0)
                        {
                            float lateChargePrice = new RentalRateDAO().GetNearestRentalRate(
                                new DiskDAO().GetDiskById(de.DiskID).TitleID, hi.CreatedDate).LateCharge;
                            totalLateCharge += lateChargePrice;
                        }
                }
            }
            return totalLateCharge;
        }

        
    }
}