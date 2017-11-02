using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface IRentalRate
    {
        RentalRate GetCurrentRentalRate(int diskTitleId);
    }
}