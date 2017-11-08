using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private CustomerDAO customerDAO;

        public CustomerService()
        {
            this.customerDAO = new CustomerDAO();

        }

        public void AddNewCustomer(Customer customer)
        {
            customerDAO.AddNewCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            customerDAO.DeleteCustomer(customer);
        }

        public List<Customer> GetAllCustomer()
        {
            return customerDAO.GetAllCustomer();
        }

        public Customer GetCustomerById(int customerId)
        {
            return customerDAO.GetCustomerById(customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
        }
    }
}