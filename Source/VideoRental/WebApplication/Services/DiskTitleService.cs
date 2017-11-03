using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.DAO;

namespace WebApplication.Services
{
    public class DiskTitleService : IDiskTitleService
    {
        private TitleDAO titleDAO;

        public DiskTitleService()
        {
            titleDAO = new TitleDAO();
        }

        public void AddNewTitle(DiskTitle title)
        {
            titleDAO.AddNewTitle(title);
        }

        public void DeleteTitle(DiskTitle title)
        {
            titleDAO.DeleteTitle(title);
        }

        public List<DiskTitle> GetAllTitles()
        {
            return titleDAO.GetAllTitles();
        }

        public DiskTitle GetTitleById(int titleId)
        {
            return titleDAO.GetTitleById(titleId);
        }
    }
}