﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using DataAccess.DAO;
using DataAccess.Entities;
using WebApplication.Services;

using DataAccess.Utilities;

namespace WebApplication.Services
{
    public class AccountService
    {
        /**
         * Check login info
         * @param loginModel : LoginModel
         * @return bool : true if login info is correct and false if not
         * */
        public virtual bool login(LoginModel loginModel)
        {
            UserDAO userDAO = new UserDAO();
            SHA2Service sha2 = new SHA2Service();
            User user = userDAO.getUserByUserName(loginModel.Username);
            if (user != null)
            {
                TagDebug.D(GetType(), sha2.Encode(loginModel.Password)+"");
                return user.Password.Equals(sha2.Encode(loginModel.Password));
            }
            return false;
        }
    }
}