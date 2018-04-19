using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ApexService.Models;
using ApexService.DataAccess;

namespace ApexService.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDB db = new EmployeeDB();
        EmployeeDetailsBO employee = new EmployeeDetailsBO();
        [HttpPost]
        public async Task<HttpResponseMessage> AddEmployeeInfo(EmployeeDetailsBO employee)
        {
            try
            {
                employee = await db.InsEmployeeDetails(employee);
                if (employee.id != 0)
                    return Request.CreateResponse(HttpStatusCode.Created, employee);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "something bad happened");
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        [HttpGet]
        [Route("api/Employee/GetEmployeeDetails/{Uid}")]
        public async Task<HttpResponseMessage> GetEmployeeDetails(int Uid)
        {
            try
            {
                if (Uid.Equals(0) || Uid.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + Uid);
                else
                {

                    employee = await db.GetEmployeeDetails(Uid);
                    if (employee != null || employee.id != 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, employee);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + Uid);
                    }
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        [HttpPut]
        [Route("api/Employee/GetEmployeeDetails/{Uid}")]
        public async Task<HttpResponseMessage> UdpateEmployeeDetails(EmployeeDetailsBO employee, int Uid)
        {
            try
            {
                if (Uid != 0)
                    employee.UserId = Uid;
                employee = await db.udpateEmployeeDetails(employee);
                if (employee != null || employee.id != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + employee.id);
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
    }
}
