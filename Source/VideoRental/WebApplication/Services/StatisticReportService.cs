using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using DataAccess.Entities;
using DataAccess.DAO;
using DataAccess.Utilities;

namespace WebApplication.Services
{
    public class StatisticReportService : IStatisticReportService
    {
        private DiskDAO diskDAO;
        private TitleDAO titleDAO;
        private ReservationDAO reservationDAO;
        public StatisticReportService()
        {
            this.diskDAO = new DiskDAO();
            this.titleDAO = new TitleDAO();
            this.reservationDAO = new ReservationDAO();
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
                listDisk = diskDAO.GetAllDiskByTitleID(title.TitleID);
                //set number of copies with each status
                foreach(Disk disk in listDisk)
                {
                    if (disk.Status.Equals(DiskStatus.RENTABLE))
                    {
                        rentable++;
                    }
                    else
                    {
                        if(disk.Status.Equals(DiskStatus.BOOKED))
                        {
                            booked++;
                        }
                        else
                        {
                            rented++;
                        }
                    }
                }
                titleModel.Total = rented + booked + rentable;
                titleModel.NumberOfInStock = rentable;
                titleModel.NumberOfOnHold = booked;
                titleModel.NumberOfRentedOut = rented;
                //set number of reservation
                titleModel.NumberOfReservation = title.Reservations.Count;
            }

            return listResult;
        }
    }
}