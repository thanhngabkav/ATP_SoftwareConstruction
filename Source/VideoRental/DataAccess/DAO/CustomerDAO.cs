using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using System.Data.Entity;
using PagedList;
using DataAccess.Utilities;

namespace DataAccess.DAO
{
    /// <summary>
    /// Customer Data Access
    /// </summary>
    public class CustomerDAO
    {
        private VideoRentalDBContext dBContext;
        public CustomerDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Add new Customer into database
        /// </summary>
        /// <param name="customer"></param>
        public void AddNewCustomer(Customer customer)
        {
            dBContext.Customers.Add(customer);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Remove a customer from database
        /// </summary>
        /// <param name="customer"></param>
        public void  DeleteCustomer(Customer customer)
        {
            dBContext.Customers.Remove(customer);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Customer customer)
        {
            dBContext.Entry(customer).State = EntityState.Modified;
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get Customer By customer's id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Single customer if found or null if not found</returns>
        public Customer GetCustomerById(int customerId)
        {
            return dBContext.Customers.Where(x => x.CustomerID == customerId).SingleOrDefault();
        }

        /// <summary>
        /// Get All customers in database
        /// </summary>
        /// <returns>List Customers</returns>
        public List<Customer> GetAllCustomer()
        {
            return dBContext.Customers.ToList();
        }

        /// <summary>
        /// Get page list customers
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>List customers</returns>
        public IPagedList<Customer> GetPageListCustomer(int page, int pageSize)
        {
            return dBContext.Customers.ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get list customers have late charge
        /// </summary>
        /// <returns></returns>
        /// 

        public List<Customer> GetListLateFeeCustomers()
        {
            TranSactionDAO tranSactionDAO = new TranSactionDAO();
            List<Customer> allCustomers = GetAllCustomer();
            List<Customer> listLateChargeCustomers = new List<Customer>();
            foreach (Customer customer in allCustomers)
            {
                bool isLateCharge = false;
                List<TransactionHistory> customerTransactions = tranSactionDAO.GetAllCustomerTransactions(customer.CustomerID);
                foreach (TransactionHistory transaction in customerTransactions)
                {
                    if (transaction.Status == null) break;
                    if (transaction.Status.Equals(TransactionStatus.DUE))
                    {
                        isLateCharge = true;
                        break;
                    }
                    
                }
                if (isLateCharge)
                {
                    listLateChargeCustomers.Add(customer);
                }
            }
            return listLateChargeCustomers;
        }


         /// <summary>
         /// Get list customers have Over dues disk
         /// </summary>
         /// <returns></returns>
        public List<Customer> GetListOverDueCustomers()
        {
            TranSactionDAO tranSactionDAO = new TranSactionDAO();
            TransactionDetailsDAO transactionDetailsDAO = new TransactionDetailsDAO();
            List<Customer> allCustomers = GetAllCustomer();
            List<Customer> listOverDueCustomers = new List<Customer>();
            foreach(Customer customer in allCustomers){
                bool isOverDue = false;
                List<TransactionHistory> customerTransactions = tranSactionDAO.GetAllCustomerTransactions(customer.CustomerID);
                foreach(TransactionHistory transaction in customerTransactions)
                {
                    RentalRateDAO rentalRateDAO = new RentalRateDAO();
                    TitleDAO titleDAO = new TitleDAO();
                    DiskDAO diskDAO = new DiskDAO();
                    List<TransactionHistoryDetail> transactionDetails = transactionDetailsDAO.GetListTransactionDetailsByTransactionId(transaction.TransactionHistoryID);
                    foreach (TransactionHistoryDetail transactionDetail in transactionDetails)
                    {
                        Disk disk = diskDAO.GetDiskById(transactionDetail.DiskID);
                        DiskTitle title = titleDAO.GetTitleById(disk.TitleID);
                        RentalRate curentRentalRate = rentalRateDAO.GetCurrentRentalRate(title.TitleID);
                        //disk is not returned on time
                        if (transactionDetail.DateReturn.Equals(null) && (DateTime.Now - transaction.CreatedDate).TotalDays > curentRentalRate.RentalPeriod )
                        {
                            isOverDue = true;
                            break;
                        }
                    }
                    if (isOverDue)
                        break;
                }
                if (isOverDue)
                    listOverDueCustomers.Add(customer);
            }
            return listOverDueCustomers;
        }

        /// <summary>
        /// Get list customer have 
        /// </summary>
        /// <param name="idorName"></param>
        /// <returns></returns>
        public List<Customer> FindCustomers(string idOrName)
        {
            return dBContext.Customers.Where((x => x.CustomerID.ToString().Contains(idOrName) || (x.FirstName + x.LastName).Contains(idOrName))).ToList();
        }



    }
}
