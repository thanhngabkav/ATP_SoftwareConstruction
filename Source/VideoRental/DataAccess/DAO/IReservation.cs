﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public interface IReservation
    {
        Reservation getAllReservation();

        void cancelReservation(String titleID, String customerID);


    }
}
