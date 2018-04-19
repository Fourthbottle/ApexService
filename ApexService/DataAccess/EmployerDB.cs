using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using ApexService.Models;
using ApexService.Generics;


namespace ApexService.DataAccess
{
    public class EmployerDB
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();



        public async Task<EmployerBO> InsEmployerDetails(EmployerBO EmpObj)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_InsEmployerDetails", con);
                cmd.Parameters.AddWithValue("@UserID", EmpObj.Userid);
                cmd.Parameters.AddWithValue("@CompName", EmpObj.CompName);
                cmd.Parameters.AddWithValue("@DUNSNumber", EmpObj.DUNSNumber);
                cmd.Parameters.AddWithValue("@SysDate", DateTime.Now); //EmpObj.SysDate);
                cmd.Parameters.AddWithValue("@Description", EmpObj.Description);

                cmd.Parameters.AddWithValue("@Street", EmpObj.EmployerAddress.street);
                cmd.Parameters.AddWithValue("@City", EmpObj.EmployerAddress.city);
                cmd.Parameters.AddWithValue("@State", EmpObj.EmployerAddress.state);
                cmd.Parameters.AddWithValue("@Country", EmpObj.EmployerAddress.country);
                cmd.Parameters.AddWithValue("@ZipCode", EmpObj.EmployerAddress.Zipcode);
                cmd.Parameters.AddWithValue("@FromDt", DateTime.Now);//EmpObj.EmployerAddress.fromdate);
                cmd.Parameters.AddWithValue("@Todate", DateTime.Now);// EmpObj.EmployerAddress.Todate);

                cmd.Parameters.AddWithValue("@RevenueAmount", EmpObj.RevenueAmount);
                cmd.Parameters.AddWithValue("@Currency", EmpObj.Currency);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        EmpObj = new EmployerBO();
                        EmpObj.Userid = Convert.ToInt32(reader["ID"]);
                        EmpObj.CompName = reader["Name"].ToString();
                        EmpObj.CompId = Convert.ToInt32(reader["CompId"]);
                        //EmpObj.AddressID = reader["AddressID"].ToString();//Convert.ToInt32(reader["AddressID"]);
                        //EmpObj.DUNSNumber = reader["DUNSNumber"].ToString();
                        //EmpObj.AboutId = Convert.ToInt32(reader["AboutId"]);
                        //EmpObj.Revenue = Convert.ToInt32(reader["Revenue"]);
                        return EmpObj;
                    }
                    else
                    { EmpObj = new EmployerBO(); return EmpObj; }
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<List<ManagedEmployer>> GetManagedEmployers(int UserId)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_GET_MANAGED_EMPLOYERS", con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<ManagedEmployer> employerList = new List<ManagedEmployer>();
                    ManagedEmployer employer;
                    while (reader.Read())
                    {
                        employer = new ManagedEmployer();
                        employer.id= Convert.ToInt32(reader["id"]);
                        employer.name = reader["Name"].ToString();
                        employer.City = reader["City"].ToString();
                        employer.Country = reader["Country"].ToString();
                        employer.State = reader["State"].ToString();
                    }
                    con.Close();
                    return employerList;
                }
            }
            catch(Exception es)
            {
                throw es;
            }
        }

        public async Task<List<EmployerBO>> GetEmployerHistory(int Eid)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_GetEmployerDetails", con);
                //cmd.Parameters.AddWithValue("@userid", Eid);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<EmployerBO> ListEmpObj = new List<EmployerBO>();
                    EmployerBO EmpObj = new EmployerBO();
                    while (reader.Read())
                    {
                        //EmpObj = new EmployerBO();
                        ////EmpObj.id = Convert.ToInt32(reader["CompId"]);
                        //EmpObj.Name = reader["Name"].ToString();
                        //EmpObj.AddressID = reader["AddressID"].ToString();
                        //EmpObj.DUNSNumber = reader["DUNSNumber"].ToString();
                        //EmpObj.AboutId = Convert.ToInt32(reader["AboutId"]);
                        //EmpObj.Revenue = Convert.ToInt32(reader["Revenue"]);
                        //EmpObj.EmployerAddress.street = reader["Street"].ToString();
                        //EmpObj.EmployerAddress.city = reader["City"].ToString();
                        //EmpObj.EmployerAddress.state = reader["State"].ToString();
                        //EmpObj.EmployerAddress.country = reader["Country"].ToString();
                        //EmpObj.EmployerAddress.Zipcode = reader["ZipCode"].ToString();
                        //EmpObj.EmployerAddress.fromdate = reader["fromDate"].ToString();
                        //EmpObj.EmployerAddress.Todate = reader["ToDate"].ToString();
                        ListEmpObj.Add(EmpObj);
                    }
                    con.Close();
                    return ListEmpObj;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<EmployerBO> updateEmployerDetails(EmployerBO EmpObj)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_UpdEmployerDetails", con);
                cmd.Parameters.AddWithValue("@UserID", EmpObj.Userid);
                cmd.Parameters.AddWithValue("@CompName", EmpObj.CompName);
                cmd.Parameters.AddWithValue("@DUNSNumber", EmpObj.DUNSNumber);
                cmd.Parameters.AddWithValue("@SysDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Description", EmpObj.Description);

                cmd.Parameters.AddWithValue("@Street", EmpObj.EmployerAddress.street);
                cmd.Parameters.AddWithValue("@City", EmpObj.EmployerAddress.city);
                cmd.Parameters.AddWithValue("@State", EmpObj.EmployerAddress.state);
                cmd.Parameters.AddWithValue("@Country", EmpObj.EmployerAddress.country);
                cmd.Parameters.AddWithValue("@ZipCode", EmpObj.EmployerAddress.Zipcode);
                cmd.Parameters.AddWithValue("@FromDt", DateTime.Now);
                cmd.Parameters.AddWithValue("@Todate", DateTime.Now);

                cmd.Parameters.AddWithValue("@RevenueAmount", EmpObj.RevenueAmount);
                cmd.Parameters.AddWithValue("@Currency", EmpObj.Currency);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        EmpObj = new EmployerBO();
                        EmpObj.Userid = Convert.ToInt32(reader["ID"]);
                        EmpObj.CompName = reader["Name"].ToString();
                        EmpObj.CompId = Convert.ToInt32(reader["CompId"]);
                        return EmpObj;
                    }
                    else
                    { EmpObj = new EmployerBO(); return EmpObj; }
                }
                
            }
            catch(Exception es)
            {
                throw es;
            }
        }

    }
}