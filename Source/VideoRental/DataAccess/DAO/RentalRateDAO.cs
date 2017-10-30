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
        public RentalRate GetCurrentRentalRate(int diskTitleId)
        {
            DateTime currentDate = DateTime.Today;
            return dbContext.RentalRates.Where(x => x.TitleID == diskTitleId).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }
    }
}
