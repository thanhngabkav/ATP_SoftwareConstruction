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
    public class ReservationService : IReservationService
    {
        ReservationDAO reservationDAO;
        CustomerDAO customerDAO;
        TitleDAO titleDAO;
        DiskDAO diskDAO;
        public ReservationService()
        {
            this.reservationDAO = new ReservationDAO();
            this.customerDAO = new CustomerDAO();
            this.titleDAO = new TitleDAO();
            this.diskDAO = new DiskDAO();
        }

        public void AddReservation(int[] titleID, int customerID)
        {
            TagDebug.D(GetType(), "in AddReservation Services");
            foreach (int atitle in titleID)
            {
                Reservation reservation = new Reservation
                {
                    CustomerID = customerID,
                    TitleID = atitle,
                    Status = ReservationStatus.IN_QUEUE,
                    ReservationDate = DateTime.Today
                };
                reservationDAO.AddReservation(reservation);
            }
        }

        public void CancelReservation(int titleID, int customerID)
        {
            TagDebug.D(GetType(), "in CancelReservation Services");
            Reservation reservation = reservationDAO.GetReservation(titleID, customerID);
            if (HasDiskForReservation(reservation))
                ChangeStatusForOnHoldDiskToRenable(reservation);
            reservationDAO.RemoveReservation(reservation);
        }

        private bool HasDiskForReservation(Reservation reservation)
        {
            return reservation.Status == ReservationStatus.ON_HOLD;
        }

        private void ChangeStatusForOnHoldDiskToRenable(Reservation reservation)
        {
            IList<Disk> disks = diskDAO.GetAllDiskByTitleID( reservation.TitleID);
            foreach (Disk disk in disks)
                if (disk.Status == DiskStatus.BOOKED)
                {
                    disk.Status = DiskStatus.RENTABLE;
                    diskDAO.UpdateDisk(disk);
                    return;
                }
                    
        }


        public bool CheckReservationForExistence(int[] titleID, int customerID)
        {
            TagDebug.D(GetType(), "in CheckReservationForExistence Services");
            foreach(int aTitle in titleID)
                if (reservationDAO.GetReservation(aTitle, customerID) != null)
                    return true;
            return false;
        }

        public IList<CustomerView> GetCustomers(string customerName)
        {
            TagDebug.D(GetType(), "in GetCustomers Services");
            if (customerName == null) customerName = "";
            IList<Customer> customers = customerDAO.FindCustomers(customerName);
            IList<CustomerView> customerViews = new List<CustomerView>();
            foreach (Customer aCustomer in customers)
                customerViews.Add(new CustomerView(aCustomer.CustomerID, aCustomer.FirstName, aCustomer.LastName, aCustomer.Address));
            return customerViews;
        }

        public List<ReservationView> GetReservation(string customerNameOrID)
        {
            TagDebug.D(GetType(), "in GetReservation Services");
            if (customerNameOrID == null) customerNameOrID = "";
            List<ReservationView> reservationViews = new List<ReservationView>();
            List<Customer> customers = customerDAO.FindCustomers(customerNameOrID);

            // getReservation from cutomer
            // get title Name from titleID
            // find diskI
            foreach (Customer aCustomer in customers)
            {
                IList<Reservation> reservations = reservationDAO.GetReservation(aCustomer.CustomerID);
                foreach (Reservation aReservation in reservations)
                {
                    reservationViews.Add(new ReservationView(aReservation.TitleID, aReservation.DiskTitle.Title, aCustomer.CustomerID, aCustomer.FirstName + " " + aCustomer.LastName, aReservation.ReservationDate, aReservation.Status));
                }
            }
            return reservationViews;
        }

        public IList<TitleView> GetTitles(string titleName)
        {
            TagDebug.D(GetType(), "in GetTitles Services");
            if (titleName == null) titleName = "";
            IList<DiskTitle> diskTitles = titleDAO.FindDiskTitles(titleName);
            IList<TitleView> titleViews = new List<TitleView>();
            foreach (DiskTitle title in diskTitles)
                titleViews.Add(new TitleView(title.TitleID, title.Title, title.Tags, title.ImageLink, title.Quantity));
            return titleViews;
        }

        public DiskTitle GetATitle(int titleID)
        {
            return titleDAO.GetTitleById(titleID);
        }
    }
}