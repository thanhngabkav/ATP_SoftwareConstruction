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
        IList<TransactionHistory> GetAllLateChargeOfCustomer(String customerID);

        /*
           * Record top n LateCharge 
           * @param: number of top latechage
           */
        void RecordLateCharge(string customerId, int numberLateCharges);

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
        void CancelLateCharge(string customerID, int numberCancel);


        /**
         * Change status of transactionHistoryDetailID to has late charge
         * @return :
         * 
         * */
        void AddLateCharge(string transactionHistoryDetailID);


    }
}