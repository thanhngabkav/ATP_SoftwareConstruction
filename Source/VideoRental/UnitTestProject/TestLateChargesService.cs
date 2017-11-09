using DataAccess.DAO;
using DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace UnitTestProject
{
    [TestClass]
    public class TestLateChargesService
    {
        ILateChargesServices late = new LateChargesService();
        [TestMethod]
        public void TestGetDisks()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            
            //Mock data access layer
            mockCustomerDAO.Setup(x => x.GetListLateFeeCustomers()).Returns(new List<Customer>());

            List<CustomerView> expectedResult = new List<CustomerView>();
            List<CustomerView> actualResult = late.FindCustomersHasLateCharge().ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetAllLateChargeOfCustomer()
        {
            var mockTranSactionDAO = new Mock<TranSactionDAO>();

            int cusID = 1;
            //Mock data access layer
            mockTranSactionDAO.Setup(x => x.GetCustomerLateChargeTransactions(cusID)).Returns(new List<TransactionHistory>());

            List<TransactionHistoryView> expectedResult = new List<TransactionHistoryView>();
            List<TransactionHistoryView> actualResult = late.GetAllLateChargeOfCustomer(cusID).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

    }
}
