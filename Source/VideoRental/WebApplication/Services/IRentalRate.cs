using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface IRentalRate
    {
        /// <summary>
        /// Get Current Rental Rate
        /// </summary>
        /// <param name="diskTitleId"></param>
        /// <returns></returns>
        RentalRate GetCurrentRentalRate(int diskTitleId);

        /// <summary>
        /// Add New Rental Rate
        /// </summary>
        /// <param name="rentalRate"></param>
        void AddNewRentalRate(RentalRate rentalRate);

        /// <summary>
        /// Get All Rental Rate
        /// </summary>
        /// <returns></returns>
        List<RentalRate> GetAllRentalRates();
    }
}