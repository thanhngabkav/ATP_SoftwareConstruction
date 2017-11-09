using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Services;
using DataAccess.DAO;
using Moq;
using WebApplication.Models;
using DataAccess.Entities;

namespace UnitTestProject
{
    [TestClass]
    public class TestAccountService
    {
        /// <summary>
        /// Sample Test Method
        /// </summary>
        [TestMethod]
        public void TestLoginFail()
        {
            AccountService accountService = new AccountService();
            Mock<UserDAO> mockUserDAO = new Mock<UserDAO>();
            LoginModel loginModel = new LoginModel { Username = "wrong user name", Password = "fake" };
            User fakeUser = new User();
            //Mock data access layer
            mockUserDAO.Setup(x => x.getUserByUserName(It.IsAny<String>())).Returns(fakeUser);
            UserDAO userDAO = new UserDAO();
            User user = userDAO.getUserByUserName(loginModel.Username);
            Assert.IsNotNull(user);
            bool expectedResult = false;
            bool actualResult = accountService.Login(loginModel);
            Assert.AreEqual(expectedResult, actualResult);
            mockUserDAO.Verify(x => x.getUserByUserName(It.IsAny<String>()), Times.AtLeastOnce);
            
        }

    }
}
