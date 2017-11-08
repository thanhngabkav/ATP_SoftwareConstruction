using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UserSession
    {
        public const string SessionName = "USERSESSION";
        public string UserID { set; get; }
        public string UserName { set; get; }
        public string PreviusURL { set; get; }
        public string UserRole { set; get; }
    }
}