using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
using WebApplication.Models;
using DataAccess.Utilities;
using System.Globalization;

namespace WebApplication.Services
{
   
    public class RentAndReturnDiskService : IRentAndReturnDiskService
    {
        private bool hasLateCharge = false;
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
            TagDebug.D(GetType(), "in GetCustomers");
            return customerDao.FindCustomers(customerID);
        }

        public IList<Disk> GetDisks(string diskName)
        {
            return diskDao.FindDisks(diskName);
        }

        public IList<DiskPriceView> GetPriceEachDisk(int[] diskID)
        {
            TagDebug.D(GetType(), "in GetPriceEachDisk");
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
            TagDebug.D(GetType(), "in GetRentedDisks");
            return diskDao.GetRentedDisks(diskID);
        }

        public void ReturnDisks(int diskID, string returnDay)
        {
            TagDebug.D(GetType(), "in ReturnDisks");
            DateTime today;
            try
            {
                today = DateTime.Parse(returnDay);
            }
            catch (Exception e)
            {
                today = DateTime.Today;
            }

            ReturnASpecificDisk(today, diskID);
        }

        private void ReturnASpecificDisk(DateTime today, int aDisk)
        {
            TagDebug.D(GetType(), "in ReturnASpecificDisk");
            Disk disk = diskDao.GetDiskById(aDisk);
            UpdateDiskStatus(disk, today);
            UpdateDateReturnTransaction(disk, today);
            UpdateLateCharge(disk, today);

        }

        private void UpdateDiskStatus(Disk disk, DateTime today)
        {
            TagDebug.D(GetType(), "in UpdateDiskStatus");
            if (HasReservationForTitle(disk))
            {
                disk.Status = DiskStatus.BOOKED;
                UpdateReservationOnHold(disk);
            }
            else
                disk.Status = DiskStatus.RENTABLE;
            diskDao.UpdateDisk(disk);
        }

        private void UpdateDateReturnTransaction(Disk disk, DateTime today)
        {
            TransactionHistoryDetail transactionDetail = transactionDetailsDAO.getTransactionDetailFromLastRentedDate(disk.DiskID);
            transactionDetail.DateReturn = today;
            transactionDetailsDAO.UpdateTransactionDetail(transactionDetail);
        }

        private bool HasReservationForTitle(Disk disk)
        {
            TagDebug.D(GetType(), "in HasReservationForTitle");
            return reservationDAO.GetNumberReservationByTitleID(disk.TitleID) > 0;
        }

        private void UpdateReservationOnHold(Disk disk)
        {
            TagDebug.D(GetType(), "in UpdateReservationOnHold");
            Reservation res = reservationDAO.GetReservationByTitleID(disk.TitleID);
            res.Status = ReservationStatus.ON_HOLD;
            reservationDAO.UpDateReservation(res);
        }


        private void UpdateLateCharge(Disk disk, DateTime today)
        {
            TagDebug.D(GetType(), "in UpdateLateCharge");
            TransactionHistoryDetail transactionHistoryDetail = transactionDetailsDAO.getTransactionDetailFromLastRentedDate(disk.DiskID);
            TransactionHistory transaction = transactionHistoryDetail.TransactionHistory;
            DateTime lastRented = disk.LastRentedDate.Value;     // conver DateTime? to DateTime
            RentalRate rentedTime = rentalRateDAO.GetNearestRentalRate(disk.TitleID,transaction.CreatedDate);
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

        public void WriteRentalDisk(int[] diskID, int customerID, int userID)
        {
            TagDebug.D(GetType(), "in WriteRentalDisk class");
            TransactionHistory transactionHistory = AddTransactionHistory(customerID, userID, diskID);
            AddTransactionDetail(diskID, customerID, transactionHistory.TransactionHistoryID);
            //
        }

        private TransactionHistory AddTransactionHistory(int customerID, int userID, int[] diskID)
        {
            TagDebug.D(GetType(), "in AddTransactionHistory class");
            TransactionHistory transaction = new TransactionHistory();
            transaction.CreatedDate = DateTime.Now;
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

        // have to test this Method
        private void AddTransactionDetail(int[] diskID, int customerID, int transactionID)
        {
            TagDebug.D(GetType(), "in AddTransactionDetail class");
            foreach (int aDiskID in diskID)
            {
                if (IsReservationExist(aDiskID, customerID))
                    RemoveReservation(aDiskID, customerID);
                AddOneTransactionDetail(aDiskID, transactionID);
                ChangeStatusCurrentDisk(aDiskID);
            }
        }

        private bool IsReservationExist(int diskID, int customerID)
        {
            DiskTitle diskTitle = titleDAO.GetTitleById(diskDao.GetDiskById(diskID).TitleID);
            Reservation reservation = reservationDAO.GetReservation(diskTitle.TitleID, customerID);
            if (reservation != null)
                return true;
            return false;
        }

        private void RemoveReservation(int aDiskID, int customerID)
        {
            reservationDAO.RemoveReservation(reservationDAO.GetReservation(diskDao.GetDiskById(aDiskID).TitleID, customerID));
        }


        private void AddOneTransactionDetail(int aDiskID, int transactionID)
        {
            TransactionHistoryDetail transactionHistoryDetail = new TransactionHistoryDetail();
            transactionHistoryDetail.DateReturn = null;
            transactionHistoryDetail.DiskID = aDiskID;
            transactionHistoryDetail.TransactionID = transactionID;
            transactionDetailsDAO.AddTransactionDetail(transactionHistoryDetail);
        }

        private void ChangeStatusCurrentDisk(int aDiskID)
        {
            Disk disk = diskDao.GetDiskById(aDiskID);
            disk.Status = DiskStatus.RENTED;
            disk.LastRentedDate = DateTime.Now;
            diskDao.UpdateDisk(disk);
        }

        public DiskTitle GetDiskTitleName(int diskTitleID)
        {
            return titleDAO.GetTitleById(diskTitleID);
        }

        public bool CheckDiskCanBeRented(int[] diskID, int customerID)
        {
            foreach (int disk in diskID)
                if (IsDiskBooked(disk))
                    if (!IsReservationExist(disk, customerID))
                        return false;
            return true;
        }

        private bool IsDiskBooked(int disk)
        {
            return diskDao.GetDiskById(disk).Status == DiskStatus.BOOKED;
        }

        public bool CheckCustomerLateCharge(int customerId)
        {
            return tranSactionDAO.CheckCustomerLateCharge(customerId);
        }

        public Disk GetADisk(int diskID)
        {
            return diskDao.GetDiskById(diskID);
        }

    }
}