using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
namespace DataAccess.DAO
{
    public interface ITransactionHistory
    {
        TransactionHistory getTransactionHistory(String customerID);
        void writeTransactionHistory(TransactionHistory transactionHistory);
        /*All customer has late Charge*/
        IList<Customer> findCustomersHasLateCharge();
 
    }
}
