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

        public void AddNewRentalRate(RentalRate rentalRate)
        {
            rentalRateDAO.AddNewRentalRate(rentalRate);
        }

        public List<RentalRate> GetAllRentalRates()
        {
            return rentalRateDAO.GetAllRentalRates();
        }

        public RentalRate GetCurrentRentalRate(int diskTitleId)
        {
            return rentalRateDAO.GetCurrentRentalRate(diskTitleId);
        }


    }
}