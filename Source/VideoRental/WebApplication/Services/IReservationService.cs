using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
namespace WebApplication.Services
{
    public interface IReservationService
    {
        /**
         * @return : Get All Reservation
         * */
        List<Reservation> GetReservation(string reservationID);

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
        void AddReservation(string[] titleName, string customerID);
    }
}