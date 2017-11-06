using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using DataAccess.DAO;
using DataAccess.Entities;
using WebApplication.Services;
namespace WebApp.Services
{
    public class AccountService
    {
        /**
         * Check login info
         * @param loginModel : LoginModel
         * @return bool : true if login info is correct and false if not
         * */
        public bool login(LoginModel loginModel)
        {
            UserDAO userDAO = new UserDAO();
            SHA2Service sha2 = new SHA2Service();
            User user = userDAO.getUserByUserName(loginModel.Username);
            if (user != null)
            {
                return user.Password.Equals(sha2.Encode(loginModel.Password));
            }
            return false;
        }
    }
}