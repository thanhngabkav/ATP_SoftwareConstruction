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

        /**
         * Get User By User Name
         * @param userName : User Name
         * @return User or null if not found
         * 
         * */
        public User getUserByUserName(String userName)
        {
            return this.userDAO.getUserByUserName(userName);
        }
    }
}