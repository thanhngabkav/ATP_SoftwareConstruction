using DataAccess.DAO;
using DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Services;

namespace UnitTestProject
{

    [TestClass]
    public class TestRentAndReturnService
    {
        /// <summary>
        /// Sample Test Method
        /// </summary>
        /// 
        IRentAndReturnDiskService rent = new RentAndReturnDiskService();
        [TestMethod]
        public void TestGetDisks()
        {
            var mockDiskDAO = new Mock<DiskDAO>();
            string diskName = "aaa";
            //Mock data access layer
            mockDiskDAO.Setup(x => x.FindDisks(diskName)).Returns(new List<Disk>());

            List<Disk> expectedResult = new List<Disk>();
            List<Disk> actualResult = rent.GetDisks(diskName).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestGetCustomers()
        {
            var mockCustomerDAO = new Mock<CustomerDAO>();
            string customer = "name";
            //Mock data access layer
            mockCustomerDAO.Setup(x => x.FindCustomers(customer)).Returns(new List<Customer>());

            List<Customer> expectedResult = new List<Customer>();
            List<Customer> actualResult = rent.GetCustomers(customer).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetRentedDisk()
        {
            var mockDiskDAO = new Mock<DiskDAO>();
            string name = "";
            mockDiskDAO.Setup(x => x.GetRentedDisks(name)).Returns(new List<Disk>());
            List<Disk> expectedResult = new List<Disk>();
            List<Disk> actualResult = rent.GetRentedDisks(name).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetDiskTitleName()
        {
            var mockDiskTitle = new Mock<TitleDAO>();
            int id = 1;
            mockDiskTitle.Setup(x => x.GetTitleById(id)).Returns(new DiskTitle { Title = "1"});

            DiskTitle expectedResult = new DiskTitle { Title = "1" };
            DiskTitle actualResult = rent.GetDiskTitleName(id);
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
