using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using System.Data.Entity;
using System.Data;
using DataAccess.Utilities;
using PagedList;
namespace DataAccess.DAO 
{
    public class UserDAO
    {
        /// <summary>
        /// User Data Access
        /// </summary>
        private VideoRentalDBContext dbContext;

        public UserDAO()
        {
            this.dbContext = new VideoRentalDBContext();
        }

     
        /// <summary>
        /// Get Single User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns> User or Null if not found</returns>
        public User getUserByUserName(String userName)
        {
            return dbContext.Users.Where(x => x.UserName.Equals(userName)).SingleOrDefault();
        }

        /// <summary>
        /// Add new User into Database
        /// </summary>
        /// <param name="user"></param>
        public void AddNewUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Remove a user from database
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Get all  User is Clerk in Database
        /// </summary>
        /// <returns>List Users </returns>
        public List<User> GetAllClerk()
        {
            return dbContext.Users.Where(x => x.Role.Equals(UserRole.Clerk)).ToList();
        }

        /// <summary>
        /// Get page list users is Clerk in database
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>List Users</returns>
        public IPagedList<User> GetPageListClerkint(int page, int pageSize){
            return dbContext.Users.Where(x => x.Role.Equals(UserRole.Clerk)).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get All Users is Manager
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllManager()
        {
            return dbContext.Users.Where(x => x.Role.Equals(UserRole.Manager)).ToList();
        }

        /// <summary>
        /// Get page list users is manager
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<User> GetPageListManager(int page, int pageSize)
        {
            return dbContext.Users.Where(x => x.Role.Equals(UserRole.Manager)).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get page list users
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<User> GetPagedListUser(int page, int pageSize)
        {
            return dbContext.Users.ToPagedList(page, pageSize);
        }
    }
}
