using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using System.Data.Entity;
using PagedList;
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

    }
}
