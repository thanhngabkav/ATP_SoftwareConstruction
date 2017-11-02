using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;

namespace WebApplication.Services
{
    public class DiskService : IDiskService
    {
        private DiskDAO diskDAO;

        public DiskService()
        {
            diskDAO = new DiskDAO();
        }

        public void AddNewDisk(Disk disk)
        {
            diskDAO.AddNewDisk(disk);
        }

        public void DeleteDisk(Disk disk)
        {
            diskDAO.DeleteDisk(disk);
        }

        public List<Disk> GetAllDisks()
        {
            return diskDAO.GetAllDisks();
        }

        public Disk GetDiskById(int diskId)
        {
            return diskDAO.GetDiskById(diskId);
        }

        public void UpdateDisk(Disk disk)
        {
            diskDAO.UpdateDisk(disk);
        }
    }
}