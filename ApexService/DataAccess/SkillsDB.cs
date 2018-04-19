using ApexService.Models;
using System.Data;
using System.Data.SqlClient;
using ApexService.Generics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ApexService.DataAccess
{
    public class SkillsDB
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public async Task<SkillsBO> AddSkill(SkillsBO skill)
        {
            try
            {
                SkillsBO skillBo = new SkillsBO();
                con = await ApexService.DataAccess.DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_Ins_Skill", con);
                cmd.Parameters.AddWithValue("@EmpId", skill.EmpId);
                cmd.Parameters.AddWithValue("@Used", skill.UsedExperience);
                cmd.Parameters.AddWithValue("@Skillset", skill.Skill);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {


                        skillBo.id = Convert.ToInt32(reader["id"]);
                        skillBo.Skill = reader["skill"].ToString();
                        skillBo.UsedExperience = Convert.ToDecimal(reader["usedYears"]);
                        skillBo.lastUPdated = Convert.ToDateTime(reader["crtDt"]);
                    }
                }
                con.Close();
                return skillBo;
            }
            catch (Exception es)
            {
                throw es;
            }
        }


        public async Task<SkillsBO> UpdateSkill(SkillsBO skill)
        {
            try
            {
                SkillsBO skillBo = new SkillsBO();
                con = await ApexService.DataAccess.DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_Upd_Skill", con);
                cmd.Parameters.AddWithValue("@EmpId", skill.EmpId);
                cmd.Parameters.AddWithValue("@Skillset", skill.Skill);
                cmd.Parameters.AddWithValue("@Used", skill.UsedExperience);
                cmd.Parameters.AddWithValue("@id", skill.id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        skillBo.id = Convert.ToInt32(reader["id"]);
                        skillBo.Skill = reader["skill"].ToString();
                        skillBo.UsedExperience = Convert.ToDecimal(reader["usedYears"]);
                        skillBo.lastUPdated = Convert.ToDateTime(reader["crtDt"]);
                    }
                }
                con.Close();
                return skillBo;

            }
            catch(Exception es)
            {
                throw es;
            }
        }


        public async Task<List<SkillsBO>> GetSkills( int EmpId)
        {
            try
            {
                SkillsBO skillBo = new SkillsBO();
                con = await ApexService.DataAccess.DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_GET_SKILLS", con);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                con.Close();
                var skillList = Conversions.ConvertDataTable<SkillsBO>(dt);
                return skillList;
            }
            catch(Exception es)
            {
                throw es;
            }
        }

        //public async Task<int> RemoveSkill(int id)
        //{

        //}
    }
}