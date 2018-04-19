using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ApexService.DataAccess;
using ApexService.Models;


namespace ApexService.Controllers
{
    public class EmployerController : ApiController
    {
        List<EmployerBO> ListEmpBo = new List<EmployerBO>();
        EmployerBO EMPObj = new EmployerBO();
        EmployerDB db = new EmployerDB();
        ManagedEmployer mngedemployer = new ManagedEmployer();
        List<ManagedEmployer> mngedEmpList = new List<ManagedEmployer>();

        [HttpPost]
        public async Task<HttpResponseMessage> InsertEmployerDetails(EmployerBO EMPBOObj)
        {
            try
            {
                EMPObj = await db.InsEmployerDetails(EMPBOObj);
                if (EMPObj.Userid != 0)
                    return Request.CreateResponse(HttpStatusCode.Created, EMPObj);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "something bad happened, please raise this to our notice");
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }


        [HttpGet]
        [Route("api/WorkHistory/GetEmployerDetails/{Eid}")]
        public async Task<HttpResponseMessage> GetEmployerDetails(int Eid)
        {
            try
            {
                if (Eid.Equals(0) || Eid.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + Eid);
                else
                {

                    ListEmpBo = await db.GetEmployerHistory(Eid);
                    if (ListEmpBo != null) //|| LWH.compId != 0
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, ListEmpBo);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + Eid);
                    }
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        public async Task<HttpResponseMessage> GetUserManagedCompanyDetails(int userId)
        {
            try
            {
                if (userId.Equals(0) || userId.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + userId);
                else
                {
                    mngedEmpList =await db.GetManagedEmployers(userId);

                    if (mngedEmpList != null)
                        return Request.CreateResponse(HttpStatusCode.OK, mngedEmpList);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No details found");
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
    }
}
