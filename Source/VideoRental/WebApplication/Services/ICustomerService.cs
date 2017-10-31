using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Add new Customer
        /// </summary>
        /// <param name="customer"></param>
        void AddNewCustomer(Customer customer);

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="customer"></param>
        void DeleteCustomer(Customer customer);

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// Get Customer By CustomerID
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns></returns>
        List<Customer> GetAllCustomer();
    }
}