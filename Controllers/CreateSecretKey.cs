using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CCMSAutomationAPI.Controllers
{
    public class CreateSecretKey
    {        
        private static string GenerateSecretKey()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            Console.WriteLine("Secret Key:" + key);
            return key;
        }
    }
}