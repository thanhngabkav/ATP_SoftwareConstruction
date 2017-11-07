using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LoginModel
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public bool Remember { set; get; }
    }
}