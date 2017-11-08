using DataAccess.DAO;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using DataAccess.Utilities;
namespace WebApplication.Services
{
    public class DiskManagementService : IDiskManagementService
    {
        private DiskDAO diskDAO;
        private TitleDAO titleDAO;
        private RentalRateDAO rentalRateDAO;
        private ReservationDAO reservationDAO;
        public DiskManagementService()
        {
            this.diskDAO = new DiskDAO();
            this.titleDAO = new TitleDAO();
            this.rentalRateDAO = new RentalRateDAO();
            this.reservationDAO = new ReservationDAO();
        }

        public DiskStatusInfoModel GetDiskStatus(int diskId)
        {
            DiskStatusInfoModel diskStatusInfo = new DiskStatusInfoModel();
            Disk disk = new Disk();
            disk = diskDAO.GetDiskById(diskId);
            diskStatusInfo.DiskID = disk.DiskID;
            diskStatusInfo.Title = disk.DiskTitle.Title;
            diskStatusInfo.Status = disk.Status;
            if (disk.Status.Equals(DiskStatus.RENTED))
            {
                //if disk rented, set for whom and when over due
                foreach (TransactionHistoryDetail transactionDetail in disk.TransactionHistoryDetails)
                {
                    TransactionHistory transaction = transactionDetail.TransactionHistory;
                    diskStatusInfo.Whom = transaction.Customer;
                    diskStatusInfo.CustomerName = transaction.Customer.LastName + " " + transaction.Customer.FirstName;
                    DiskTitle title = titleDAO.GetTitleById(disk.TitleID);
                    RentalRate curentRentalRate = rentalRateDAO.GetCurrentRentalRate(title.TitleID);
                    DateTime dateReturn = (transaction.CreatedDate).AddDays(curentRentalRate.RentalPeriod);
                    diskStatusInfo.DueTime = dateReturn;
                }
            }
            else
            {
                // if disk booked, who did book? => set for whom booked
                if (disk.Status.Equals(DiskStatus.BOOKED))
                {
                    List<Reservation> reservations = reservationDAO.GetListReservationByTitle(disk.TitleID);
                    foreach (var item in reservations)
                    {
                        //get first customer booked
                        if (item.Status.Equals(ReservationStatus.ON_HOLD))
                        {
                            diskStatusInfo.Whom = item.Customer;
                            diskStatusInfo.CustomerName = item.Customer.LastName + " " + item.Customer.FirstName;
                            break;
                        }
                    }
                    diskStatusInfo.DueTime = null;
                }
                else // disk in stock, toWhom and when over due
                {
                    diskStatusInfo.Whom = null;
                    diskStatusInfo.DueTime = null;
                }
            }
            return diskStatusInfo;
        }
    }
}