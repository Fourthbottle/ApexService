using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApexService.Models;
using ApexService.DataAccess;
using ApexService.Generics;
using System.Threading;


namespace ApexService.Controllers
{
    public class RegistrationController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Register(RegistrationBO regDetails)
        {
            try
            {
                RegisterDB regDB = new RegisterDB();
                regDetails = await regDB.register(regDetails);
                if (regDetails.isExists)
                    return Request.CreateResponse(HttpStatusCode.Conflict,FailureResponse.Message="Email already registered");
                else
                    return  Request.CreateResponse(HttpStatusCode.Created, regDetails);
            }
            catch (Exception es)
            {
                return  Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
    }
}
