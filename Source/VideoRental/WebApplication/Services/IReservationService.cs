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
        List<Customer> GetCustomers(string customerName);

        /**
         * @return : Get All Title
         * */
        List<DiskTitle> GetTitles(string titleName);


        /**
        * Add a reservation
        * @return : 
        * */
        void AddReservation(string[] titleID, string customerID);

        /**
        * Cancel a reservation,change status disk to free
        * @return : 
        * */
        void CancelReservation(string titleID, string customerID);
    }
}