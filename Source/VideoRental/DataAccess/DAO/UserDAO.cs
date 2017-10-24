using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
namespace DataAccess.DAO
{
    public class UserDAO
    {
        private VideoRentalDBContext dbContext;

        public UserDAO()
        {
            this.dbContext = new VideoRentalDBContext();
        }
        /**
         * Get Single User By UserName
         * @param userName : User Name
         * @return : User or Null if not found
         * */
        public User getUserByUserName(String userName)
        {
            return dbContext.Users.Where(x => x.UserName.Equals(userName)).SingleOrDefault();
        }

    }
}
