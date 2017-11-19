using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IReservationService
    {
        // if not found return to length = 0
        
        /**
         * @return : Get All Reservation
         * */
        List<ReservationView> GetReservation(string customerNameOrID );

        /**
         * @return : Get All Customer
         * */
        IList<CustomerView> GetCustomers(string customerName);

        /**
         * @return : Get All Title
         * */
        IList<TitleView> GetTitles(string titleName);
        /**
        * @return : Get Title by ID
        * */
        DiskTitle GetATitle(int titleID);

        /**
        * Add a reservation
        * @return : 
        * */
        void AddReservation(int[] titleID, int customerID);

        /**
        * Cancel a reservation,change status disk to free
        * @return : 
        * */
        void CancelReservation(int titleID, int customerID);

        /*
         * Check Reservation Exist
         *
         */
        bool CheckReservationForExistence(int[] titleID, int customerID);
    }
}