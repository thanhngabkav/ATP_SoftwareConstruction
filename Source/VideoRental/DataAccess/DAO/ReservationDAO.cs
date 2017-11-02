using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Utilities;
using DataAccess.DBContext;
using DataAccess.Entities;


namespace DataAccess.DAO
{
    public class ReservationDAO
    {
        private VideoRentalDBContext dBContext;
        public ReservationDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Add new reservation
        /// </summary>
        /// <param name="reservation"></param>
        public void AddReservation(Reservation reservation)
        {
            dBContext.Reservations.Add(reservation);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get first num Reservations
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<Reservation> GetReservations(int num)
        {
            return dBContext.Reservations.Take(num).ToList();
        }

        /// <summary>
        /// Remove a reservation from database
        /// </summary>
        /// <param name="reservation"></param>
        public void RemoveReservation (Reservation reservation)
        {
            dBContext.Reservations.Remove(reservation);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get Reservation by customer id and title id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="diskTitleId"></param>
        /// <returns></returns>
        public Reservation GetReservation(int customerId, int diskTitleId)
        {
            return dBContext.Reservations.Where(x => x.CustomerID == customerId && x.TitleID == diskTitleId).FirstOrDefault();
        }


        /// <summary>
        /// Get Reservation by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="diskTitleId"></param>
        /// <returns></returns>
        public List<Reservation> GetReservation(int customerId)
        {
            return dBContext.Reservations.Where(x => x.CustomerID == customerId).ToList();
        }
    }
}
