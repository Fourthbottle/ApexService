using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApexService.Models;
using System.Data;
using System.Data.SqlClient;
using ApexService.Generics;
using System.Threading.Tasks;


namespace ApexService.DataAccess
{
    public class RegisterDB
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public async Task<RegistrationBO> register(RegistrationBO regDetails)
        {
            con = await ApexService.DataAccess.DBConnection.ApexConnection();
            cmd = new DBConnection().BuildProcedureCommand("P_INS_User",con);
            cmd.Parameters.AddWithValue("@Email", regDetails.email);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Password", new Authenticate().Encryptdata(regDetails.password));
            cmd.Parameters.AddWithValue("@VerificationCode", Guid.NewGuid().ToString());
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    regDetails.id = reader.GetInt32(reader.GetOrdinal("id"));
                    regDetails.password = "";
                    regDetails.email = reader.GetString(1);
                    regDetails.isEmailVerified = reader.GetInt32(2).Equals(1) ? true : false;
                    regDetails.isExists = reader.GetString(3).Equals("Y") ? true : false;
                    regDetails.VerficationCode = reader.GetString(4);
                }
            }
            con.Close();
            return regDetails;
        }
      
        public async Task<LoginBO> Login(LoginBO login)
        {
            try
            {
                con = await ApexService.DataAccess.DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_SEL_LOGIN", con);
                cmd.Parameters.AddWithValue("@EMAIL", login.email);
                cmd.Parameters.AddWithValue("@PASSWORD", new Authenticate().Encryptdata(login.password));
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        login.id = reader.GetInt32(0);
                        login.email = reader.GetString(1).ToString();
                        login.password = "";
                        login.isEmailVerified = reader.GetInt32(2).Equals(1) ? true : false;
                        login.VerificationCode = reader.GetString(3);
                        login.Empid = reader.GetInt32(4);
                    }
                }
                con.Close();
                return login;
            }
            catch(Exception es)
            {
                throw es;
            }
        }
    }
}