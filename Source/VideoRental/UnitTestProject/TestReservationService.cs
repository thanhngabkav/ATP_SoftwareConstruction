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
    public class TestReservationService
    {
        IReservationService reservation = new ReservationService();
        [TestMethod]
        public void TestGetReservation()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            string customerNameOrID = "1";
            //Mock data access layer
            mockCustomerDAO.Setup(x => x.FindCustomers(customerNameOrID)).Returns(new List<Customer>());

            List<ReservationView> expectedResult = new List<ReservationView>();
            List<ReservationView> actualResult = reservation.GetReservation(customerNameOrID).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetCustomer()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            string customerName = "tt";
            //Mock data access layer
            mockCustomerDAO.Setup(x => x.FindCustomers(customerName)).Returns(new List<Customer>());

            List<CustomerView> expectedResult = new List<CustomerView>();
            List<CustomerView> actualResult = reservation.GetCustomers(customerName).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetTitles()
        {
            var mockTitleDAO = new Mock<TitleDAO>();
            string titleName = "tt";
            //Mock data access layer
            mockTitleDAO.Setup(x => x.FindDiskTitles(titleName)).Returns(new List<DiskTitle>());

            List<TitleView> expectedResult = new List<TitleView>();
            List<TitleView> actualResult = reservation.GetTitles(titleName).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }


    }
}
