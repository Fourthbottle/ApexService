using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;

namespace ApexService.DataAccess
{
    public class DBConnection
    {
        public async static Task<SqlConnection> ApexConnection()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApexDB"].ToString());
                await con.OpenAsync();
                return con;
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public SqlCommand BuildProcedureCommand(string procName, SqlConnection con)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = procName;
                return cmd;
            }
            catch (Exception es)
            {
                throw es;
            }
        }
    }
}