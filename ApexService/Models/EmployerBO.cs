using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexService.Models
{
    public class EmployerBO
    {
        public EmployerBO()
        {
            EmployerAddress = new EmployerAddress();
        }
        public int Userid { get; set; }
        public string CompName { get; set; }

        public int CompId { get; set; }
        public string DUNSNumber { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string Description { get; set; }

        public EmployerAddress EmployerAddress;
        public int RevenueAmount { get; set; }
        public string Currency { get; set; }
    }
    public class EmployerAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string Zipcode { get; set; }
 
    }

    public class ManagedEmployer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    
}