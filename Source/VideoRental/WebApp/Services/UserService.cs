using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Entities;
using DAL.DataAccess;
namespace WebApp.Services
{
    public class UserService
    {
        private UserDAO userDAO;

        public UserService()
        {
            this.userDAO = new UserDAO();
        }
        public User getUserByUserName(String userName)
        {
            return this.userDAO.getUserByUserName(userName);
        }
    }
}