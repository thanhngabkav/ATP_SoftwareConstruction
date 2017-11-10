using DataAccess.DAO;
using DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Services;

namespace UnitTestProject
{
    [TestClass]
    public class TestCustomerReportService
    {
        [TestMethod]
        public void TestReport_AllCustomer()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            CustomerReportService customerReportService = new CustomerReportService(mockCustomerDAO.Object);
            //sample data
            List<Customer> listCustomer = new List<Customer>();
            List<TransactionHistory> listTransaction = new List<TransactionHistory>();
            listCustomer.Add(new Customer
            {
                CustomerID = 1,
                FirstName = "Firstname1",
                LastName = "Lastname1",
                Address = "Address1",
                PhoneNumber = "0123456",
                TransactionHistorys = listTransaction
            });
            listCustomer.Add(new Customer
            {
                CustomerID = 2,
                FirstName = "Firstname2",
                LastName = "Lastname2",
                Address = "Address2",
                PhoneNumber = "0123456",
                TransactionHistorys = listTransaction
            });
            mockCustomerDAO.Setup(x => x.GetAllCustomer()).Returns(listCustomer);
            //create expected result
            List<CustomerReportModel> expectedResult = new List<CustomerReportModel>();
            expectedResult.Add(new CustomerReportModel
            {
                CustomerID = 1,
                CustomerName = "Lastname1 Firstname1",
                Address = "Address1",
                PhoneNumber = "0123456",
                DiskOverDues = new List<DiskOverDueModel>(),
                LateCharges = new List<LateCharge>(),
                TotalDisk = 0,
                TotalFines = 0
            });
            expectedResult.Add(new CustomerReportModel
            {
                CustomerID = 2,
                CustomerName = "Lastname2 Firstname2",
                Address = "Address2",
                PhoneNumber = "0123456",
                DiskOverDues = new List<DiskOverDueModel>(),
                LateCharges = new List<LateCharge>(),
                TotalDisk = 0,
                TotalFines = 0
            });
            //create actual Result
            List<CustomerReportModel> actualResult = customerReportService.Report_AllCustomer();
            //compare
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

        }
        [TestMethod]
        public void TestReport_LateFeeCustomer()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            CustomerReportService customerReportService = new CustomerReportService(mockCustomerDAO.Object);
            //sample data
            List<Customer> listCustomer = new List<Customer>();
            mockCustomerDAO.Setup(x => x.GetListLateFeeCustomers()).Returns(listCustomer);
            //create expected result
            List<CustomerReportModel> expectedResult = new List<CustomerReportModel>();
            //create actual Result
            List<CustomerReportModel> actualResult = customerReportService.Report_LateFeeCustomer();
            //compare
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

        }
        [TestMethod]
        public void TestReport_OverDueCustomer()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            CustomerReportService customerReportService = new CustomerReportService(mockCustomerDAO.Object);
            //sample data
            List<Customer> listCustomer = new List<Customer>();
            mockCustomerDAO.Setup(x => x.GetListOverDueCustomers()).Returns(listCustomer);
            //create expected result
            List<CustomerReportModel> expectedResult = new List<CustomerReportModel>();
            //create actual Result
            List<CustomerReportModel> actualResult = customerReportService.Report_OverDueCustomer();
            //compare
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

        }
    }
}
