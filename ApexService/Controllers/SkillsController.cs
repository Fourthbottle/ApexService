using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using System.Web.Http;
using ApexService.Models;
using ApexService.DataAccess;
using System.Collections.Generic;

namespace ApexService.Controllers
{
    public class SkillsController : ApiController
    {
        SkillsDB db = new SkillsDB();
        SkillsBO skillsObj = new SkillsBO();
        List<SkillsBO> skillList = new List<SkillsBO>();
        public async Task<HttpResponseMessage> POST(SkillsBO skills)
        {
            try
            {
                skillsObj = await db.AddSkill(skills);
                if (skillsObj.id != 0)
                    return Request.CreateResponse(HttpStatusCode.Created, skillsObj);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "something bad happened, please raise this to our notice");
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                var skillsets = await db.GetSkills(id);

                if (skillsets.Count != 0)
                    return Request.CreateResponse(HttpStatusCode.OK, skillsets);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No details found for EmpId " + id);
            }
            catch (Exception es)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, es.Message);
            }
        }
    }
}
