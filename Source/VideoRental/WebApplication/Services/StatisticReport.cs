using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using DataAccess.Entities;
using DataAccess.DAO;
namespace WebApplication.Services
{
    public class StatisticReport : IStatisticReport
    {
        private DiskDAO diskDAO;
        private TitleDAO titleDAO;
        private ReservationDAO reservationDAO;
        public StatisticReport()
        {
        }

        public List<TitleReportModel> Report_Title()
        {
            List<TitleReportModel> listResult = new List<TitleReportModel>();
            List<DiskTitle> listAllTitle = titleDAO.GetAllTitles();
            foreach (DiskTitle title in listAllTitle)
            {
                TitleReportModel titleModel = new TitleReportModel();
                titleModel.TitleID = title.TitleID;
                titleModel.Title = title.Title;
                int rentable = 0;
                int rented = 0;
                int booked = 0;
                List<Disk> listDisk = new List<Disk>();
                //foreach list disk of it'title. 
                //
                //foreach get
            }

            return listResult;
        }
    }
}