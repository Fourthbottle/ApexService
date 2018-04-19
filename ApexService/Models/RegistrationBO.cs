using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexService.Models
{
    public class RegistrationBO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isEmailVerified { get; set; }
        public string VerficationCode { get; set; }
        public bool isExists { get; set; }
            
    }
}