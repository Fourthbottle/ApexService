using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexService.Models
{
    public class WorkHistoryBO
    {
        public WorkHistoryBO()
        {
            WorkRoles = new WorkRolesBO();
            manager = new ManagerDetails();
            workLocation = new WorkAddress();
        }

        public int? compId { get; set; }
        public string compName { get; set; }
        public WorkAddress workLocation { get; set; }
        public ManagerDetails manager { get; set; }
        public string Responsibility { get; set; }
        public string EmploymentId { get; set; }
        public WorkRolesBO WorkRoles { get; set; }
        public int? WHID { get; set; }
        public int EmpId { get; set; }
    }

    public class WorkAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string Zipcode { get; set; }
    }
    public class ManagerDetails
    {
        public string MgrFullName { get; set; }
        public string MgrEmail { get; set; }
        public string MgrCountryCode { get; set; }
        public string MgrMobileNumb { get; set; }
        public int MgrisVerified { get; set; }
    }

    public class WorkRolesBO
    {
        public int? WHID { get; set; }
        public DateTime FromDate { get; set; }
        public string ToDate { get; set; }
        public string Designation { get; set; }
        public int EmpType { get; set; }
        public int isCurrentCompany { get; set; }
        public int isCompVerified { get; set; }
    }

  
}