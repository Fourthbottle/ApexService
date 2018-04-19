using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApexService.DataAccess;
using System.Web.Http;
using ApexService.Models;

namespace ApexService.Controllers
{
    public class WorkHistoryController : ApiController
    {
        WorkHistoryDB db = new WorkHistoryDB();
        WorkHistoryBO WH = new WorkHistoryBO();
        List<WorkHistoryBO> LWH = new List<WorkHistoryBO>();
        public async Task<HttpResponseMessage> InsertWorkHistory(WorkHistoryBO workHistory)
        {
            try
            {
                WH = await db.InsEmployeeDetails(workHistory);
                if (WH.compId != 0)
                    return Request.CreateResponse(HttpStatusCode.Created, WH);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "something bad happened, please raise this to our notice");
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        public async Task<HttpResponseMessage> getWorkHistory(int WHID, int EmpId)
        {
            try
            {
                if (WHID.Equals(0) || WHID.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + WHID);
                else if((EmpId.Equals(0) || EmpId.Equals(null)))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + WHID);
                else
                {
                    WH = await db.getWorkHistory(WHID, EmpId);
                    if(WH != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, WH);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + WHID);
                    }
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        [HttpGet]
        [Route("api/WorkHistory/GetWorkHistory/{Uid}")]
        public async Task<HttpResponseMessage> GetWorkHistory(int Uid)
        {
            try
            {
                if (Uid.Equals(0) || Uid.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + Uid);
                else
                {

                    LWH = await db.GetWorkHistory(Uid);
                    if (LWH != null) //|| LWH.compId != 0
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, LWH);
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
        [Route("api/WorkHistory/updateWorkHistory")]
        public async Task<HttpResponseMessage> updateWorkHistory(WorkHistoryBO workHistory)
        {
            try
            {
                if (workHistory.WHID.Equals(0) || workHistory.WHID.Equals(null))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for given id : " + workHistory.WHID);
                else
                {
                    int result = await db.UpdateWorkHistory(workHistory);
                    switch (result)
                    {
                        case 0:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "details Not updated");
                        case 1:
                            return Request.CreateResponse(HttpStatusCode.OK, "details updated successfully");
                        case 2:
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Not Authorized to udpate these details");
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "details Not updated");
                    }
                }
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> deleteWorkHistory(int id, int EmpId)
        {
            try
            {
                if (EmpId == 0 || EmpId == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "unable to delete work History");
                else if (id == 0 || id== null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "unable to delete work History");
                else
                {
                    bool result = await db.DeleteWorkHistory(id, EmpId);
                    if (result)
                        return Request.CreateResponse(HttpStatusCode.OK, "success");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in deleting work history");
                }
            }
            catch(Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
    }
}
