using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication.Services
{
    public class SHA2Service
    {
        public string Encode(string input)
        {
            SHA256 mySha = SHA256Managed.Create();
            return Convert.ToBase64String(mySha.ComputeHash(Encoding.Unicode.GetBytes(input)));
        }
    }
}