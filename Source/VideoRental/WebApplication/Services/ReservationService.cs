using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ReservationService : IReservationService
    {
        ReservationDAO reservationDAO;
        CustomerDAO customerDAO;
        TitleDAO titleDAO;
        public ReservationService()
        {
            this.reservationDAO = new ReservationDAO();
            this.customerDAO = new CustomerDAO();
            this.titleDAO = new TitleDAO();
        }

        public void AddReservation(string[] titleID, string customerID)
        {
            foreach (string atitle in titleID)
            {
                Reservation reservation = new Reservation
                {
                    CustomerID = Int32.Parse(customerID),
                    TitleID = Int32.Parse(atitle),
                    ReservationDate = DateTime.Today
                };
                reservationDAO.AddReservation(reservation);
            }
        }

        public void CancelReservation(string titleID, string customerID)
        {
            Reservation reservation = reservationDAO.GetReservation(Int32.Parse(titleID), Int32.Parse(customerID));
            reservationDAO.RemoveReservation(reservation);
        }

        public List<Customer> GetCustomers(string customerName)
        {
            return customerDAO.FindCustomers(customerName);
        }

        public List<ReservationView> GetReservation(string customerNameOrID)
        {
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
                    reservationViews.Add(new ReservationView(aReservation.TitleID, aReservation.DiskTitle.Title, aCustomer.CustomerID, aCustomer.FirstName + " " + aCustomer.LastName, aReservation.ReservationDate));
                }
            }
            return reservationViews;
        }

        public List<DiskTitle> GetTitles(string titleName)
        {
            return titleDAO.FindDiskTitles(titleName);
        }
    }
}