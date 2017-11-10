using DataAccess.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApplication.Services;
using DataAccess.Entities;
using WebApplication.Models;

namespace UnitTestProject
{
    [TestClass]
    public class TestTitleManagementService
    {
        [TestMethod]
        public void TestGetInfomationTitle()
        {
            var mockTitleDAO = new Mock<TitleDAO>();
            TitleManagementService titleManagementService = new TitleManagementService(mockTitleDAO.Object);
            //sample input
            int titleID = 1;
            //sample data
            List<Disk> Disks = new List<Disk>();
            DiskTitle title = new DiskTitle
            {
                TitleID = titleID,
                Title = "Sample title",
                ImageLink = "Default",
                Quantity = 0,
                Tags = "",
                Disks = Disks
            };
            mockTitleDAO.Setup(x => x.GetTitleById(titleID)).Returns(title);
            //create expected result
            TitleInfoModel expectedResult = new TitleInfoModel {
                TitleID = 1,
                Title = "Sample title",
                Quantity = 0,
                ImageLink = "Default",
                NumberOfDiskRentable = 0
            };
            //actualResult 
            TitleInfoModel actualResult = titleManagementService.GetInfomationTitle(titleID);
            //Compare
            Assert.AreEqual(expectedResult.TitleID, actualResult.TitleID);
            Assert.AreEqual(expectedResult.Title, actualResult.Title);
            Assert.AreEqual(expectedResult.Quantity, actualResult.Quantity);
            Assert.AreEqual(expectedResult.ImageLink, actualResult.ImageLink);
            Assert.AreEqual(expectedResult.NumberOfDiskRentable, actualResult.NumberOfDiskRentable);
        }
    }
}
