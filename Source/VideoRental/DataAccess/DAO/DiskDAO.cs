using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.DBContext;
using System.Data.Entity;
using DataAccess.Utilities;

namespace DataAccess.DAO
{
    public class DiskDAO
    {
        private VideoRentalDBContext dBContext;

        public DiskDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }

        /// <summary>
        /// Get first num disks is rented in database
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<Disk> GetRentedDisks(int num)
        {
            return dBContext.Disks.Where(x => x.Status.Equals(DiskStatus.RENTED)).Take(num).ToList();
        }

        /// <summary>
        /// Update Disk
        /// </summary>
        /// <param name="disk"></param>
        public void UpdateDisk(Disk disk)
        {
            dBContext.Entry(disk).State = EntityState.Modified;
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Add new disk
        /// </summary>
        /// <param name="disk"></param>
        public void AddNewDisk(Disk disk)
        {
            dBContext.Disks.Add(disk);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Delete Disk from database  (not include transaction detail)
        /// </summary>
        /// <param name="disk"></param>
        public void DeleteDisk(Disk disk)
        {
            dBContext.Disks.Remove(disk);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Get All Disks in database
        /// </summary>
        /// <returns></returns>
        public List<Disk> GetAllDisks()
        {
            return dBContext.Disks.ToList();
        }   

        /// <summary>
        /// Get disk by disk's id
        /// </summary>
        /// <param name="diskId"></param>
        /// <returns>Single disk if found or null if not found</returns>
        public Disk GetDiskById(int diskId)
        {
            return dBContext.Disks.Where(x => x.DiskID == diskId).SingleOrDefault();
        }
    }
}
