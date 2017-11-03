using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface IDiskService
    {
        /// <summary>
        /// Add New Disk
        /// </summary>
        /// <param name="disk"></param>
        void AddNewDisk(Disk disk);

        /// <summary>
        /// Delete Disk
        /// </summary>
        /// <param name="disk"></param>
        void DeleteDisk(Disk disk);

        /// <summary>
        /// Update Disk
        /// </summary>
        /// <param name="disk"></param>
        void UpdateDisk(Disk disk);

        /// <summary>
        /// Get All Disk
        /// </summary>
        /// <returns></returns>
        List<Disk> GetAllDisks();

        /// <summary>
        /// Get Disk By DiskID
        /// </summary>
        /// <param name="diskId"></param>
        /// <returns></returns>
        Disk GetDiskById(int diskId);

    }
}