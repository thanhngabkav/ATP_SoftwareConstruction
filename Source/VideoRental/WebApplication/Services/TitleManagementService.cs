using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using DataAccess.Entities;
using DataAccess.Utilities;

namespace WebApplication.Services
{
    public class TitleManagementService : ITitleManagementService
    {
        TitleDAO titleDAO;
        public TitleInfoModel GetInfomationTitle(int titleID)
        {
            DiskTitle title = titleDAO.GetTitleById(titleID);
            TitleInfoModel result = new TitleInfoModel();
            result.TitleID = title.TitleID;
            result.Title = title.Title;
            result.Quantity = title.Quantity;
            result.RentalPrice = title.RentalPrice;
            result.LateChargePerDate = title.LateChargePerDate;
            int rentable = 0;
            foreach (Disk disk in title.Disks)
            {
                if (disk.Status.Equals(DiskStatus.RENTABLE)) rentable++;
            }
            result.NumberOfDiskRentable = rentable;
            return result;
        }
    }
}