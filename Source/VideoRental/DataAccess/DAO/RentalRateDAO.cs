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
    public class RentalRateDAO
    {
        private VideoRentalDBContext dbContext;

        public RentalRateDAO()
        {
            this.dbContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Get current Rentalrate 
        /// </summary>
        /// <param name="diskTitleId"></param>
        /// <returns></returns>
        public virtual RentalRate GetCurrentRentalRate(int diskTitleId)
        {
            DateTime currentDate = DateTime.Today;
            return dbContext.RentalRates.Where(x => x.TitleID == diskTitleId).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }

        /// <summary>
        /// Get Nearest RentalRate 
        /// </summary>
        /// <param name="diskTitleId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual RentalRate GetNearestRentalRate(int diskTitleId, DateTime date)
        {
            List<RentalRate> titleRentalRates = dbContext.RentalRates.Where(x => x.TitleID == diskTitleId).OrderByDescending(x => x.CreatedDate).ToList();
            foreach(RentalRate rentalRate in titleRentalRates)
            {
                if (date > rentalRate.CreatedDate)
                    return rentalRate;
            }
            return titleRentalRates[0];
        }

        /// <summary>
        /// Add new Rentalrate into database
        /// </summary>
        /// <param name="rentalRate"></param>
        public virtual void AddNewRentalRate(RentalRate rentalRate)
        {
            dbContext.RentalRates.Add(rentalRate);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Get All Rentalrates in database
        /// </summary>
        /// <returns></returns>
        public virtual List<RentalRate> GetAllRentalRates()
        {
            return dbContext.RentalRates.ToList();
        }
    }
}
