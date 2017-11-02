using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IRentAndReturnDiskService
    {
        /**
         * @param no param
         * @return : Get List of Disk contain diskName ~ %like%
         * */
        IList<Disk> GetDisks(string diskName);

        /**
         * @param no param
         * @return : Get List of Disk contain customer ~ %like%
         * */
        IList<Customer> GetCustomers(string customer);

        /**
         * @param no param
         * @return : a Pair include Disk Name And Its price.
         * */
        IList<DiskPriceView> GetPriceEachDisk(string[] diskID);

        /**
         * Occurs after the customer completes the rental in View
         * Write Transaction to Database
         * @return : 
         * */
        void WriteRentalDisk(string[] diskID, string customerID, string userID);

        /**
         * @return : get All disk that the status has been rented
         * If diskID is null, replace by empty string
         * */
        IList<Disk> GetRentedDisks(string diskID);

        /**
         * @return : Change status of  list disk to free
         * 
         * */
        void ReturnDisks(string[] diskID);

        /**
         * @return :List latecharge of customer
         * 
         * */
        IList<TransactionHistory> ShowLateCharge(string customerID);
    }
}