using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
namespace WebApplication.Services
{
    public interface IRentAndReturnDiskService
    {
        /**
         * 
         * @param no param
         * @return : Get List of Disk contain diskName ~ %like%
         * */
        IList<Disk> GetDisks(String diskName);

        /**
         * @param no param
         * @return : Get List of Disk contain customer ~ %like%
         * */
        IList<Customer> GetCustomers(String customer);

        /**
         * @param no param
         * @return : a Pair include Disk Name And Its price.
         * */
        Tuple<String, String> GetPriceEachDisk(String[] diskID);

        /**
         * Occurs after the customer completes the rental in View
         * Write Transaction to Database
         * @return : 
         * */
        void WriteRentingDisk(String[] diskID, String customerID);

    }
}