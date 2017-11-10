using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IAccountService
    {
        /**
        * Check login info
        * @param loginModel : LoginModel
        * @return bool : true if login info is correct and false if not
        * */
        bool Login(LoginModel loginModel);
    }
}