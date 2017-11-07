using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string input = "manager";
            SHA256 mySha = SHA256Managed.Create();

            Console.WriteLine(Convert.ToBase64String(mySha.ComputeHash(Encoding.Unicode.GetBytes(input))));
        }
    }
}
