using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCMSAutomationAPI.Models
{
    public class TokenResponse
    {
        public string TransRmk { get; set; }
        public bool TransStatus { get; set; }
        public string AccessToken { get; set; }
       
    }
}