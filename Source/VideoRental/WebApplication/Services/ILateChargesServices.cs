using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface ILateChargesServices
    {
        /**
         * 
         * @param userName : 
         * @return : List of Customer is having late charge
         * */
        IList<Customer> FindCustomersHasLateCharge();
    
    }
}