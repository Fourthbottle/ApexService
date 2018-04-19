using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexService.Models;
using ApexService.DataAccess;
using ApexService.Generics;
using System.Threading.Tasks;

namespace ApexService.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Login(LoginBO login)
        {
            try
            {
                Authenticate aut = new Authenticate();
                RegisterDB regDB = new RegisterDB();
                login =await regDB.Login(login);

                if (login.id.Equals(0))
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, FailureResponse.Message="Userid password missmatch");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, login);
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }

    }
}
