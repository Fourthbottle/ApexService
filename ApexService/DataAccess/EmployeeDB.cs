using ApexService.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApexService.DataAccess
{
    public class EmployeeDB
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public async Task<EmployeeDetailsBO> GetEmployeeDetails(int Uid)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_SEL_GET_EMPLOYEE_DETAILS", con);
                cmd.Parameters.AddWithValue("@userid", Uid);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    EmployeeDetailsBO emp = new EmployeeDetailsBO();
                    if (reader.Read())
                    {
                        emp.countryCode = reader["CountryCode"].ToString();
                        emp.MobileNumber = reader["Number"].ToString();
                        emp.FirstName = reader["FirstName"].ToString();
                        emp.LastName = reader["LastName"].ToString();
                        emp.SSN = reader["SSN"].ToString();
                        emp.id = Convert.ToInt32(reader["Empid"]);
                    }
                    con.Close();
                    return emp;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<EmployeeDetailsBO> InsEmployeeDetails(EmployeeDetailsBO employee)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_Ins_Employee", con);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@SSN", employee.SSN);
                cmd.Parameters.AddWithValue("@countryCode", employee.countryCode);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@Userid", employee.UserId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        employee.id = Convert.ToInt32(reader["id"]);
                        employee.UserId = Convert.ToInt32(reader["Userid"]);
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.LastName = reader["Lastname"].ToString();
                        employee.MobileNumber = reader["CountryCode"].ToString();
                        employee.countryCode = reader["Mobile"].ToString();
                    }
                }
                con.Close();
                return employee;
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<EmployeeDetailsBO> udpateEmployeeDetails(EmployeeDetailsBO employee)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_Ins_Employee", con);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@SSN", employee.SSN);
                cmd.Parameters.AddWithValue("@countryCode", employee.countryCode);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@Userid", employee.UserId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        employee = new EmployeeDetailsBO();
                        employee.countryCode = reader["COUNTRYCODE"].ToString();
                        employee.MobileNumber = reader["Mobile"].ToString();
                        employee.FirstName = reader["FIRSTNAME"].ToString();
                        employee.LastName = reader["LASTNAME"].ToString();
                        employee.SSN = reader["SSN"].ToString();
                        employee.id = Convert.ToInt32(reader["ID"]);
                    }
                }
                con.Close();
                return employee;
            }
            catch (Exception es)
            {
                throw es;
            }
        }
    }
}