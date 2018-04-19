using ApexService.DataAccess;
using ApexService.Generics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApexService.Models
{
    public class LoginBO
    {
        public int id { get; set; }
        public string email { get; set; }
        public bool isEmailVerified { get; set; }
        public string password { get; set; }
        public string VerificationCode { get; set; }
        public int? Empid { get; set; }

    }
}