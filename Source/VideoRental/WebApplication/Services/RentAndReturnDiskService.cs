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

    public class RentAndReturnDiskService : IRentAndReturnDiskService
    {
        private static string TAG = "RentAndReturnDiskService: ";
        CustomerDAO customerDao;
        DiskDAO diskDao;
        TitleDAO titleDAO;
        TranSactionDAO tranSactionDAO;
        TransactionDetailsDAO transactionDetailsDAO;
        public RentAndReturnDiskService()
        {
            customerDao = new CustomerDAO();
            diskDao = new DiskDAO();
            titleDAO = new TitleDAO();
            tranSactionDAO = new TranSactionDAO();
            transactionDetailsDAO = new TransactionDetailsDAO();
        }
        public IList<Customer> GetCustomers(string customerID)
        {
            TagDebug.D(GetType(), "in GetCustomers class");
            return customerDao.FindCustomers(customerID);
        }

        public IList<Disk> GetDisks(string diskName)
        {
            return diskDao.FindDisks(diskName);
        }

        public IList<DiskPriceView> GetPriceEachDisk(string[] diskID)
        {
            TagDebug.D(GetType(), "in GetPriceEachDisk class");
            IList<DiskPriceView> diskPriceViews = new List<DiskPriceView>();
            foreach (string d in diskID)
            {
                Disk disk = diskDao.GetDiskById(Int32.Parse(d));
                DiskTitle diskTitle = titleDAO.GetTitleById(disk.TitleID);
                diskPriceViews.Add(new DiskPriceView(d, diskTitle.Title, disk.PurchasePrice));
            }
            return diskPriceViews;
        }

        public IList<Disk> GetRentedDisks(string diskID)
        {
            TagDebug.D(GetType(), "in GetRentedDisks class");
            return diskDao.GetRentedDisks(diskID);
        }

        public void ReturnDisks(string[] diskID)
        {
            TagDebug.D(GetType(), "in ReturnDisks class");
            DateTime today = DateTime.Today;
            foreach (string aDisk in diskID)
                ReturnASpecificDisk(today, aDisk);

        }

        private void ReturnASpecificDisk(DateTime today, string aDisk)
        {
            TagDebug.D(GetType(), "in ReturnASpecificDisk class");
            Disk disk = diskDao.GetDiskById(Int32.Parse(aDisk));
            int rangeDay = today.Subtract(disk.LastRentedDate).Days;
            if (isLateCharge(rangeDay, disk.RentedTime))
                addSpecificLateCharge(disk);
            disk.Status = DiskStatus.RENTABLE;
        }

        private bool isLateCharge(int rangeDay, int rentedTime)
        {
            return rangeDay > rentedTime ? true : false;
        }

        private void addSpecificLateCharge(Disk disk)
        {
            TagDebug.D(GetType(), "in addSpecificLateCharge class");
            TransactionHistoryDetail transactionDetails = transactionDetailsDAO.getTransactionDetailFromLastRentedDate(disk.DiskID);
            UpdateDueToTransactionDetail(transactionDetails);
            UpdateDueToTransaction(transactionDetails);
        }

        private void UpdateDueToTransactionDetail(TransactionHistoryDetail transactionDetails)
        {
            transactionDetails.Status = TransactionStatus.DUE;
            transactionDetailsDAO.UpdateTransactionDetail(transactionDetails);
        }

        private void UpdateDueToTransaction(TransactionHistoryDetail transactionDetails)
        {
            TransactionHistory transaction = tranSactionDAO.GetTransaction(transactionDetails.TransactionID);
            transaction.Status = TransactionStatus.DUE;
            tranSactionDAO.UpdateTransaction(transaction);
        }

        public IList<TransactionHistory> ShowLateCharge(string customerID)
        {
            TagDebug.D(GetType(), "in ShowLateCharge class");
            return null;
        }

        public void WriteRentalDisk(string[] diskID, int customerID, int userID)
        {
            TagDebug.D(GetType(), "in WriteRentalDisk class");
            TransactionHistory transactionHistory = AddTransactionHistory(customerID, userID, diskID);
            AddTransactionDetail(diskID, transactionHistory.TransactionHistoryID);
            //
        }

        private TransactionHistory AddTransactionHistory(int customerID, int userID, string[] diskID)
        {
            TagDebug.D(GetType(), "in AddTransactionHistory class");
            TransactionHistory transaction = new TransactionHistory();
            transaction.CreatedDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            transaction.Status = TransactionStatus.CANCELED;
            transaction.ClerkID = userID;
            transaction.CustomerID = customerID;
            transaction.TotalPurchaseCost = CalculateCost(diskID);
            tranSactionDAO.AddnewTransaction(transaction);
            return transaction;
        }
        private float CalculateCost(string[] diskID)
        {
            float cost = 0;
            IList<DiskPriceView> listPriceDisk = GetPriceEachDisk(diskID);
            foreach (DiskPriceView d in listPriceDisk)
                cost += d.price;
            return cost;
        }

        private void AddTransactionDetail(string[] diskID, int transactionID)
        {
            TagDebug.D(GetType(), "in AddTransactionDetail class");
            foreach (string aDiskID in diskID)
            {
                TransactionHistoryDetail transactionHistoryDetail = new TransactionHistoryDetail();
                transactionHistoryDetail.DateReturn = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                transactionHistoryDetail.DiskID = Int32.Parse(aDiskID);
                transactionHistoryDetail.Status = TransactionStatus.CANCELED;
                transactionHistoryDetail.TransactionID = transactionID;
                transactionDetailsDAO.AddTransactionDetail(transactionHistoryDetail);
            }
        }

        public DiskTitle getDiskTitleName(int diskTitleID)
        {
            return titleDAO.GetTitleById(diskTitleID);
        }
    }
}