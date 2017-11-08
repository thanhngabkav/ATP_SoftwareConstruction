using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Utilities;
using DataAccess.DBContext;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public class TitleDAO
    {
        private VideoRentalDBContext dBContext;

        public TitleDAO()
        {
            this.dBContext = new VideoRentalDBContext();
        }
    
        /// <summary>
        /// Get All Titles
        /// </summary>
        /// <returns></returns>
        public virtual List<DiskTitle> GetAllTitles()
        {
            return dBContext.DiskTitles.ToList();
        }

        /// <summary>
        /// Uodate title
        /// </summary>
        /// <param name="title"></param>
        public virtual void UpdateTitle(DiskTitle title)
        {
            dBContext.Entry(title).State = EntityState.Modified;
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Add new title
        /// </summary>
        /// <param name="title"></param>
        public virtual void AddNewTitle(DiskTitle title)
        {
            dBContext.DiskTitles.Add(title);
            dBContext.SaveChanges();
        }

        /// <summary>
        /// Delete a title from database (not include disks)
        /// </summary>
        /// <param name="title"></param>
        public virtual void DeleteTitle(DiskTitle title)
        {
            dBContext.DiskTitles.Remove(title);
        }

        /// <summary>
        /// Get Disk title by Id
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        public virtual DiskTitle GetTitleById(int titleId)
        {
            return dBContext.DiskTitles.Where(x => x.TitleID == titleId).SingleOrDefault();
        }

        /// <summary>
        /// Get titles have id or name contain input
        /// </summary>
        /// <param name="idOrName"></param>
        /// <returns></returns>
        public virtual List<DiskTitle> FindDiskTitles(string idOrName)
        {
            return dBContext.DiskTitles.Where(x => x.TitleID.ToString().Contains(idOrName) || x.Title.Contains(idOrName)).ToList();
        }


    }
}
