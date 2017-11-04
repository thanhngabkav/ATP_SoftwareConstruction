using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.DAO;
using DataAccess.Entities;

namespace WebApplication.Services
{
    public interface IDiskTitleService
    {
        /// <summary>
        /// Add New Title
        /// </summary>
        /// <param name="title"></param>
        void AddNewTitle(DiskTitle title);

        /// <summary>
        /// Delete Title
        /// </summary>
        /// <param name="title"></param>
        void DeleteTitle(DiskTitle title);

        /// <summary>
        /// Get All Title
        /// </summary>
        /// <returns></returns>
        List<DiskTitle> GetAllTitles();

        /// <summary>
        /// Get Title By TitleID
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        DiskTitle GetTitleById(int titleId);
    }
}