﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using DataAccess.DAO;
using DataAccess.Entities;
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
            User user = userDAO.getUserByUserName(loginModel.Username);
            if (user != null)
            {
                return user.Password.Equals(loginModel.Password);
            }
            return false;
        }
    }
}