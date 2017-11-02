using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;

namespace WebApplication.Services
{
    public class RentalRateService : IRentalRate
    {
        private RentalRateDAO rentalRateDAO;

        public RentalRateService()
        {
            this.rentalRateDAO = new RentalRateDAO();
        }

        public RentalRate GetCurrentRentalRate(int diskTitleId)
        {
            return rentalRateDAO.GetCurrentRentalRate(diskTitleId);
        }
    }
}