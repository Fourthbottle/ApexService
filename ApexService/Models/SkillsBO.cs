using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexService.Models
{
    public class SkillsBO
    {
        public int id { get; set; }
        public string Skill { get; set; }
        public int EmpId { get; set; }
        public decimal UsedExperience { get; set; }
        public DateTime lastUPdated { get; set; }
    }
}