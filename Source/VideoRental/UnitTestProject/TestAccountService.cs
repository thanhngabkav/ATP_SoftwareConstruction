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
            LoginModel loginModel = new LoginModel { Username = "wrong user name", Password = "fake" };
            User fakeUser = new User { Password = "user password" };

            //Mock data access layer
            var mockUserDAO = new Mock<UserDAO>();
            AccountService accountService = new AccountService(mockUserDAO.Object);
            mockUserDAO.Setup(x => x.getUserByUserName(loginModel.Username)).Returns(fakeUser);

            bool expectedResult = false;
            bool actualResult = accountService.Login(loginModel);
            Assert.AreEqual(expectedResult, actualResult);
            mockUserDAO.Verify(x => x.getUserByUserName(loginModel.Username), Times.AtLeastOnce);
            
        }

        [TestMethod]
        public void TestLoginPass()
        {
            LoginModel loginModel = new LoginModel { Username = "wrong user name", Password = "User pass word" };
            SHA2Service sha2 = new SHA2Service();
            String encodePassword = sha2.Encode("User pass word");
            User fakeUser = new User { Password = encodePassword };

            //Mock data access layer
            var mockUserDAO = new Mock<UserDAO>();
            AccountService accountService = new AccountService(mockUserDAO.Object);
            mockUserDAO.Setup(x => x.getUserByUserName(loginModel.Username)).Returns(fakeUser);

            bool expectedResult = true;
            bool actualResult = accountService.Login(loginModel);
            Assert.AreEqual(expectedResult, actualResult);
            mockUserDAO.Verify(x => x.getUserByUserName(loginModel.Username), Times.AtLeastOnce);
        }

    }
}
