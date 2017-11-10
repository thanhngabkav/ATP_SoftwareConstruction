using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.DAO;
using Moq;
using WebApplication.Services;
using System.Collections.Generic;
using DataAccess.Entities;
using DataAccess.Utilities;
using WebApplication.Models;

namespace UnitTestProject
{
    [TestClass]
    public class TestStatisticReportService
    {
        [TestMethod]
        public void TestReport_Title()
        {
            var mockTitleDAO = new Mock<TitleDAO>();
            var mockDiskDAO = new Mock<DiskDAO>();
            StatisticReportService statisticReportService = new StatisticReportService(mockTitleDAO.Object, mockDiskDAO.Object);
            //sample input
            List<Disk> Disks = new List<Disk>();
            Disks.Add(new Disk
            {
                DiskID = 1,
                Status = DiskStatus.RENTABLE
            });
            Disks.Add(new Disk
            {
                DiskID = 2,
                Status = DiskStatus.BOOKED
            });
            Disks.Add(new Disk
            {
                DiskID = 3,
                Status = DiskStatus.RENTED
            });
            List<DiskTitle> listTitle = new List<DiskTitle>();
            listTitle.Add(new DiskTitle
            {
                TitleID = 1,
                Title = "Title sample",
                Disks = Disks,
                Reservations = new List<Reservation>()
            });
            mockTitleDAO.Setup(x => x.GetAllTitles()).Returns(listTitle);
            mockDiskDAO.Setup(x => x.GetAllDiskByTitleID(It.IsAny<int>())).Returns(Disks);
            //create expected result
            List<TitleReportModel> expectedResult = new List<TitleReportModel>();
            expectedResult.Add(new TitleReportModel {
                TitleID = 1,
                Title = "Title sample",
                Total = 3,
                NumberOfInStock = 1,
                NumberOfOnHold = 1,
                NumberOfRentedOut = 1,
                NumberOfReservation = 0
            });
            //create actualResult
            List<TitleReportModel> actualResult = statisticReportService.Report_Title();
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }
    }
}
