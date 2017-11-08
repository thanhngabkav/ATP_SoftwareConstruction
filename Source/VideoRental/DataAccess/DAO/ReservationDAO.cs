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
        public virtual void AddReservation(Reservation reservation)
        {
            dBContext.Reservations.Add(reservation);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get first num Reservations
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public virtual List<Reservation> GetReservations(int num)
        {
            return dBContext.Reservations.Take(num).ToList();
        }

        /// <summary>
        /// Get Reservation by title id And Sort by date
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public virtual Reservation GetReservationByTitleID(int titleID)
        {
            return dBContext.Reservations.Where(x => x.TitleID == titleID).OrderBy(x=>x.ReservationDate).FirstOrDefault();
        }


        /// <summary>
        /// Get number Reservation by title id And Sort by date
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public virtual int GetNumberReservationByTitleID(int titleID)
        {
            return dBContext.Reservations.Where(x => x.TitleID == titleID && x.Status == ReservationStatus.IN_QUEUE).Count();
        }
        /// <summary>
        /// Remove a reservation from database
        /// </summary>
        /// <param name="reservation"></param>
        public virtual void RemoveReservation (Reservation reservation)
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
        public virtual Reservation GetReservation(int diskTitleId, int customerId)
        {
            return dBContext.Reservations.Where(x => x.CustomerID == customerId && x.TitleID == diskTitleId).FirstOrDefault();
        }


        /// <summary>
        /// Get Reservation by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="diskTitleId"></param>
        /// <returns></returns>
        public virtual List<Reservation> GetReservation(int customerId)
        {
            return dBContext.Reservations.Where(x => x.CustomerID == customerId).ToList();
        }

        public virtual void UpDateReservation(Reservation res)
        {
            dBContext.Entry(res).State = EntityState.Modified;
            dBContext.SaveChanges();
        }

        public List<Reservation> GetListReservationByTitle(int titleId)
        {
            return dBContext.Reservations.Where(x => x.TitleID == titleId).ToList<Reservation>();
        }
    }
}
