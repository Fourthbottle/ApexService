using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApexService.Controllers
{
    public class AddressController : ApiController
    {
        // GET: api/Address
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Address/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Address
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Address/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Address/5
        public void Delete(int id)
        {
        }
    }
}
