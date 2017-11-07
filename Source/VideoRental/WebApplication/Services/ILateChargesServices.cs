using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;
using WebApplication.Models;
namespace WebApplication.Services
{
    public interface ILateChargesServices
    {
        /**
         * 
         * @param userName : 
         * @return : List of Customer is having late charge, unless return to length = 0
         * */
        IList<CustomerView> FindCustomersHasLateCharge();

        /**
         *  Sequence Name: GetLateCharge(customerID);
         * @param userName : 
         * @return : Get All Late Charge Of A Customer, unless return to length = 0
         * */
        IList<TransactionHistoryView> GetAllLateChargeOfCustomer(int customerID);

        /*
           * Record top n LateCharge 
           * @param: number of top latechage
           */
        void RecordLateCharge(int customerId, int numberLateCharges);

        /**
         * @return :List latecharge of customer
         * 
         * */
        IList<TransactionHistory> ShowLateCharge(string customerID);

        /**
         * Change status of transactionHistoryDetailID to free or something else 
         * @return :
         * 
         * */
        void CancelLateCharge(int transactionHistoryID);


        /**
         * Change status of transactionHistoryDetailID to has late charge
         * @return :
         * 
         * */
        void AddLateCharge(string transactionHistoryDetailID);

        /*
         * Count Number of Customer's LateCharge 
         */
        int GetNumberOfLateCharge(int customerID);
        /*
         * Get Total Pric
         */
        float GetTotalLateChargePrice(int customerId, int numberLateCharges);
    }
}