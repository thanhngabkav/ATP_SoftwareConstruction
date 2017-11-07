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
        CustomerDAO customerDao;
        DiskDAO diskDao;
        TitleDAO titleDAO;
        TranSactionDAO tranSactionDAO;
        TransactionDetailsDAO transactionDetailsDAO;
        RentalRateDAO rentalRateDAO;
        ReservationDAO reservationDAO;
        public RentAndReturnDiskService()
        {
            customerDao = new CustomerDAO();
            diskDao = new DiskDAO();
            titleDAO = new TitleDAO();
            tranSactionDAO = new TranSactionDAO();
            transactionDetailsDAO = new TransactionDetailsDAO();
            rentalRateDAO = new RentalRateDAO();
            reservationDAO = new ReservationDAO();
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

        public IList<DiskPriceView> GetPriceEachDisk(int[] diskID)
        {
            TagDebug.D(GetType(), "in GetPriceEachDisk class");
            IList<DiskPriceView> diskPriceViews = new List<DiskPriceView>();
            foreach (int d in diskID)
            {
                Disk disk = diskDao.GetDiskById(d);
                DiskTitle diskTitle = titleDAO.GetTitleById(disk.TitleID);
                RentalRate price = rentalRateDAO.GetCurrentRentalRate(diskTitle.TitleID);
                diskPriceViews.Add(new DiskPriceView(d, diskTitle.Title, price.RentalPrice));
            }
            return diskPriceViews;
        }

        public IList<Disk> GetRentedDisks(string diskID)
        {
            TagDebug.D(GetType(), "in GetRentedDisks class");
            return diskDao.GetRentedDisks(diskID);
        }

        public void ReturnDisks(int diskID)
        {
            TagDebug.D(GetType(), "in ReturnDisks class");
            DateTime today = DateTime.Today;
            ReturnASpecificDisk(today, diskID);
        }

        private void ReturnASpecificDisk(DateTime today, int aDisk)
        {
            TagDebug.D(GetType(), "in ReturnASpecificDisk class");
            Disk disk = diskDao.GetDiskById(aDisk);
            UpdateDiskStatus(disk, today);
            UpdateLateCharge(disk, today);

        }

        private void UpdateDiskStatus(Disk disk, DateTime today)
        {
            if (HasReservationForTitle(disk))
            {
                disk.Status = DiskStatus.BOOKED;
                UpdateReservationOnHold(disk);
            }else
                disk.Status = DiskStatus.RENTABLE;
            diskDao.UpdateDisk(disk);
        }

        private bool HasReservationForTitle(Disk disk)
        {
            return reservationDAO.GetNumberReservationByTitleID(disk.TitleID) > 0;
        }

        private void UpdateReservationOnHold(Disk disk)
        {
            Reservation res = reservationDAO.GetReservationByTitleID(disk.TitleID);
            res.Status = ReservationStatus.ON_HOLD;
            reservationDAO.UpDateReservation(res);
        }


        private void UpdateLateCharge(Disk disk, DateTime today)
        {
            DateTime lastRented = disk.LastRentedDate.Value;     // conver DateTime? to DateTime
            RentalRate rentedTime = rentalRateDAO.GetCurrentRentalRate(disk.TitleID);
            int rangeDay = today.Subtract(lastRented).Days;
            if (IsLateCharge(rangeDay, rentedTime.RentalPeriod))
                AddSpecificLateCharge(disk);
        }

        private bool IsLateCharge(int rangeDay, int rentedTime)
        {
            return rangeDay > rentedTime ? true : false;
        }

        private void AddSpecificLateCharge(Disk disk)
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

        public void WriteRentalDisk(int[] diskID, int customerID, int userID)
        {
            TagDebug.D(GetType(), "in WriteRentalDisk class");
            TransactionHistory transactionHistory = AddTransactionHistory(customerID, userID, diskID);
            AddTransactionDetail(diskID, transactionHistory.TransactionHistoryID);
            //
        }

        private TransactionHistory AddTransactionHistory(int customerID, int userID, int[] diskID)
        {
            TagDebug.D(GetType(), "in AddTransactionHistory class");
            TransactionHistory transaction = new TransactionHistory();
            transaction.CreatedDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            transaction.ClerkID = userID;
            transaction.CustomerID = customerID;
            transaction.TotalPurchaseCost = CalculateCost(diskID);
            tranSactionDAO.AddnewTransaction(transaction);
            return transaction;
        }
        private float CalculateCost(int[] diskID)
        {
            float cost = 0;
            IList<DiskPriceView> listPriceDisk = GetPriceEachDisk(diskID);
            foreach (DiskPriceView d in listPriceDisk)
                cost += d.price;
            return cost;
        }

        private void AddTransactionDetail(int[] diskID, int transactionID)
        {
            TagDebug.D(GetType(), "in AddTransactionDetail class");
            foreach (int aDiskID in diskID)
            {
                TransactionHistoryDetail transactionHistoryDetail = new TransactionHistoryDetail();
                transactionHistoryDetail.DateReturn = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                transactionHistoryDetail.DiskID = aDiskID;
                transactionHistoryDetail.TransactionID = transactionID;
                transactionDetailsDAO.AddTransactionDetail(transactionHistoryDetail);

                ChangeStatusCurrentDisk(aDiskID);
            }
        }

        private void ChangeStatusCurrentDisk(int aDiskID)
        {
            Disk disk = diskDao.GetDiskById(aDiskID);
            disk.Status = DiskStatus.RENTED;
            disk.LastRentedDate = DateTime.Now;
            diskDao.UpdateDisk(disk);
        }

        public DiskTitle getDiskTitleName(int diskTitleID)
        {
            return titleDAO.GetTitleById(diskTitleID);
        }
    }
}