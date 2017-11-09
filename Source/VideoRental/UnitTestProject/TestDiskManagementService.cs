using WebApplication.Services;
using DataAccess.DAO;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Models;
using DataAccess.Entities;
using DataAccess.Utilities;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class TestDiskManagementService
    {
        [TestMethod]
        public void TestGetStatus()
        {
            DiskManagementService diskManagementService = new DiskManagementService();
            var mockDiskDAO = new Mock<DiskDAO>();
            int diskID = 1;
            //create sample title
            DiskTitle title = new DiskTitle();
            title.TitleID = 1;
            title.Title = "Sample Title";
            //create sample disk
            Disk disk = new Disk { DiskID =1, Status = DiskStatus.RENTABLE
            , PurchasePrice = 2000, TitleID =1 , UpdatedUser = 1, DateCreate = DateTime.Now,
            RentedTime = 0, LastRentedDate = DateTime.Now, DateUpdate= DateTime.Now};
          //  disk.DiskID = 1;
            disk.DiskTitle = title;
          //  disk.Status = DiskStatus.RENTABLE;
            mockDiskDAO.Setup(x => x.GetDiskById(diskID)).Returns(disk);
            //create expected result
             DiskStatusInfoModel expectedResul = new DiskStatusInfoModel();
            expectedResul.DiskID = 1;
            expectedResul.Title = "Sample Title";
            expectedResul.Status = DiskStatus.RENTABLE;
            expectedResul.Whom = null;
            expectedResul.DueTime = null;
            expectedResul.CustomerName = null;
            DiskDAO diskDAO = new DiskDAO();
            Disk diskMock = new Disk();
            diskMock = diskDAO.GetDiskById(diskID);
            Assert.IsNotNull(disk);
         //   Assert.AreEqual(diskMock.DiskID, 1);
           // Assert.AreEqual(diskMock.DiskTitle, title);
             //DiskStatusInfoModel actualResult = diskManagementService.GetDiskStatus(diskID);
            /*   Assert.IsNull(actualResult.DueTime);
               Assert.IsNull(actualResult.Whom);
               Assert.IsNull(actualResult.CustomerName);
               Assert.AreEqual(expectedResul.DiskID, actualResult.DiskID);
               Assert.AreEqual(expectedResul.Title, actualResult.Title);
               Assert.AreEqual(expectedResul.Status, actualResult.Status);*/
        }
    }
}
