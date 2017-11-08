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
            var mockUserDAO = new Mock<UserDAO>();
            LoginModel loginModel = new LoginModel { Username = "wrong user name", Password = "fake" };
            User fakeUser = null;
            //Mock data access layer
            mockUserDAO.Setup(x => x.getUserByUserName(loginModel.Username)).Returns(fakeUser);

            bool expectedResult = false;
            bool actualResult = accountService.login(loginModel);
            Assert.AreEqual(expectedResult, actualResult);
            
        }

    }
}
