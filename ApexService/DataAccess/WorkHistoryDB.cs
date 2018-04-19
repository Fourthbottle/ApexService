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
    public class WorkHistoryDB
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public async Task<WorkHistoryBO> InsEmployeeDetails(WorkHistoryBO workHistory)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_InsWorkHistory", con);
                cmd.Parameters.AddWithValue("@CompId", workHistory.compId);
                cmd.Parameters.AddWithValue("@CompName", workHistory.compName);
                cmd.Parameters.AddWithValue("@Street", workHistory.workLocation.street);
                cmd.Parameters.AddWithValue("@City", workHistory.workLocation.city);
                cmd.Parameters.AddWithValue("@State", workHistory.workLocation.state);
                cmd.Parameters.AddWithValue("@Country", workHistory.workLocation.country);
                cmd.Parameters.AddWithValue("@Zipcode", workHistory.workLocation.Zipcode);
                cmd.Parameters.AddWithValue("@ManagerFullName", workHistory.manager.MgrFullName);
                cmd.Parameters.AddWithValue("@managerEmail", workHistory.manager.MgrEmail);
                cmd.Parameters.AddWithValue("@managerCountryCode", workHistory.manager.MgrCountryCode);
                cmd.Parameters.AddWithValue("@managerMobilenumb", workHistory.manager.MgrMobileNumb);
                cmd.Parameters.AddWithValue("@RolesRespon", workHistory.Responsibility);
                cmd.Parameters.AddWithValue("@Empid", workHistory.EmpId);
                cmd.Parameters.AddWithValue("@fromDate", workHistory.WorkRoles.FromDate);
                cmd.Parameters.AddWithValue("@toDate", workHistory.WorkRoles.ToDate);
                cmd.Parameters.AddWithValue("@designation", workHistory.WorkRoles.Designation);
                cmd.Parameters.AddWithValue("@empType", workHistory.WorkRoles.EmpType);
                cmd.Parameters.AddWithValue("@iscurrentCompany", workHistory.WorkRoles.isCurrentCompany);
                cmd.Parameters.AddWithValue("@EmploymentID", workHistory.EmploymentId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        workHistory.compId = Convert.ToInt32(reader["CompId"]);
                        workHistory.compName = reader["CompanyName"].ToString();
                        workHistory.workLocation.street = reader["WLStreet"].ToString();
                        workHistory.workLocation.city = reader["WLcity"].ToString();
                        workHistory.workLocation.state = reader["WLstate"].ToString();
                        workHistory.workLocation.country = reader["WLCountry"].ToString();
                        workHistory.workLocation.Zipcode = reader["WLzipcode"].ToString();
                        workHistory.manager.MgrFullName = reader["ManagerName"].ToString();
                        workHistory.manager.MgrEmail = reader["ManagerEmail"].ToString();
                        workHistory.manager.MgrCountryCode = reader["ManagerCountryCode"].ToString();
                        workHistory.manager.MgrMobileNumb = reader["managerMobNumb"].ToString();
                        workHistory.manager.MgrisVerified = Convert.ToInt32(reader["ManagerVerified"]);
                        workHistory.Responsibility = reader["RolesAndResp"].ToString();
                        workHistory.EmploymentId = reader["EmploymentID"].ToString();
                        workHistory.WorkRoles.FromDate = Convert.ToDateTime(reader["FromDate"]);
                        workHistory.WorkRoles.isCurrentCompany = Convert.ToInt32(reader["isCurrent"]);
                        workHistory.WorkRoles.ToDate = Convert.ToInt32(reader["isCurrent"]).Equals(1) ? null : reader["ToDate"].ToString();
                        workHistory.WorkRoles.Designation = reader["Designation"].ToString();
                        workHistory.WorkRoles.isCompVerified = Convert.ToInt32(reader["CompanyVerified"]);
                        workHistory.WorkRoles.EmpType = Convert.ToInt32(reader["EmpType"]);
                        workHistory.WHID = Convert.ToInt32(reader["WHID"]);

                        return workHistory;
                    }
                    else
                    { WorkHistoryBO WH = new WorkHistoryBO(); return WH; }
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<WorkHistoryBO> getWorkHistory(int WHID, int EmpId)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("p_Get_WorkHistory", con);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@WHID", WHID);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    WorkHistoryBO workHistory = new WorkHistoryBO();
                    while (reader.Read())
                    {
                        workHistory.compId = Convert.ToInt32(reader["CompId"]);
                        workHistory.compName = reader["CompanyName"].ToString();
                        workHistory.workLocation.street = reader["WLStreet"].ToString();
                        workHistory.workLocation.city = reader["WLcity"].ToString();
                        workHistory.workLocation.state = reader["WLstate"].ToString();
                        workHistory.workLocation.country = reader["WLCountry"].ToString();
                        workHistory.workLocation.Zipcode = reader["WLzipcode"].ToString();
                        workHistory.manager.MgrFullName = reader["ManagerName"].ToString();
                        workHistory.manager.MgrEmail = reader["ManagerEmail"].ToString();
                        workHistory.manager.MgrCountryCode = reader["ManagerCountryCode"].ToString();
                        workHistory.manager.MgrMobileNumb = reader["managerMobNumb"].ToString();
                        workHistory.manager.MgrisVerified = Convert.ToInt32(reader["ManagerVerified"]);
                        workHistory.Responsibility = reader["RolesAndResp"].ToString();
                        workHistory.EmploymentId = reader["EmploymentID"].ToString();
                        workHistory.WorkRoles.FromDate = Convert.ToDateTime(reader["FromDate"]);
                        workHistory.WorkRoles.isCurrentCompany = Convert.ToInt32(reader["isCurrent"]);
                        workHistory.WorkRoles.ToDate = Convert.ToInt32(reader["isCurrent"]).Equals(1) ? null : reader["ToDate"].ToString();
                        workHistory.WorkRoles.Designation = reader["Designation"].ToString();
                        workHistory.WorkRoles.isCompVerified = Convert.ToInt32(reader["CompanyVerified"]);
                        workHistory.WorkRoles.EmpType = Convert.ToInt32(reader["EmpType"]);
                        workHistory.WHID = Convert.ToInt32(reader["WHID"]);
                        workHistory.EmpId = Convert.ToInt32(reader["EmpId"]);
                        return workHistory;
                    }
                    con.Close();
                    return workHistory;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<List<WorkHistoryBO>> GetWorkHistory(int EmpId)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_GetWorkRoles", con);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    List<WorkHistoryBO> LWorkHist = new List<WorkHistoryBO>();
                    WorkHistoryBO workHistory;//ssss = new WorkHistoryBO();
                    while (reader.Read())
                    {
                        workHistory = new WorkHistoryBO();
                        workHistory.compId = Convert.ToInt32(reader["CompId"]);
                        workHistory.compName = reader["CompanyName"].ToString();
                        workHistory.workLocation.street = reader["WLStreet"].ToString();
                        workHistory.workLocation.city = reader["WLcity"].ToString();
                        workHistory.workLocation.state = reader["WLstate"].ToString();
                        workHistory.workLocation.country = reader["WLCountry"].ToString();
                        workHistory.workLocation.Zipcode = reader["WLzipcode"].ToString();
                        workHistory.manager.MgrFullName = reader["ManagerName"].ToString();
                        workHistory.manager.MgrEmail = reader["ManagerEmail"].ToString();
                        workHistory.manager.MgrCountryCode = reader["ManagerCountryCode"].ToString();
                        workHistory.manager.MgrMobileNumb = reader["managerMobNumb"].ToString();
                        workHistory.manager.MgrisVerified = Convert.ToInt32(reader["ManagerVerified"]);
                        workHistory.Responsibility = reader["RolesAndResp"].ToString();
                        workHistory.EmploymentId = reader["EmploymentID"].ToString();
                        workHistory.WorkRoles.FromDate = Convert.ToDateTime(reader["FromDate"]);
                        workHistory.WorkRoles.isCurrentCompany = Convert.ToInt32(reader["isCurrent"]);
                        workHistory.WorkRoles.ToDate = Convert.ToInt32(reader["isCurrent"]).Equals(1) ? null : reader["ToDate"].ToString();
                        workHistory.WorkRoles.Designation = reader["Designation"].ToString();
                        workHistory.WorkRoles.isCompVerified = Convert.ToInt32(reader["CompanyVerified"]);
                        workHistory.WorkRoles.EmpType = Convert.ToInt32(reader["EmpType"]);
                        workHistory.WHID = Convert.ToInt32(reader["WHID"]);
                        workHistory.EmpId = Convert.ToInt32(reader["EmpId"]);
                        LWorkHist.Add(workHistory);
                    }
                    con.Close();
                    return LWorkHist;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<int> UpdateWorkHistory( WorkHistoryBO workHistory)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_UPD_WORKHISTORY", con);
                cmd.Parameters.AddWithValue("@CompId", workHistory.compId);
                cmd.Parameters.AddWithValue("@CompName", workHistory.compName);
                cmd.Parameters.AddWithValue("@Street", workHistory.workLocation.street);
                cmd.Parameters.AddWithValue("@City", workHistory.workLocation.city);
                cmd.Parameters.AddWithValue("@State", workHistory.workLocation.state);
                cmd.Parameters.AddWithValue("@Country", workHistory.workLocation.country);
                cmd.Parameters.AddWithValue("@Zipcode", workHistory.workLocation.Zipcode);
                cmd.Parameters.AddWithValue("@ManagerFullName", workHistory.manager.MgrFullName);
                cmd.Parameters.AddWithValue("@managerEmail", workHistory.manager.MgrEmail);
                cmd.Parameters.AddWithValue("@managerCountryCode", workHistory.manager.MgrCountryCode);
                cmd.Parameters.AddWithValue("@managerMobilenumb", workHistory.manager.MgrMobileNumb);
                cmd.Parameters.AddWithValue("@RolesRespon", workHistory.Responsibility);
                cmd.Parameters.AddWithValue("@Empid", workHistory.EmpId);
                cmd.Parameters.AddWithValue("@fromDate", workHistory.WorkRoles.FromDate);
                cmd.Parameters.AddWithValue("@toDate", workHistory.WorkRoles.ToDate);
                cmd.Parameters.AddWithValue("@designation", workHistory.WorkRoles.Designation);
                cmd.Parameters.AddWithValue("@empType", workHistory.WorkRoles.EmpType);
                cmd.Parameters.AddWithValue("@iscurrentCompany", workHistory.WorkRoles.isCurrentCompany);
                cmd.Parameters.AddWithValue("@EmploymentID", workHistory.EmploymentId);
                cmd.Parameters.AddWithValue("@Whid", workHistory.WHID);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                        return 0;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        public async Task<bool> DeleteWorkHistory ( int id, int EmpId)
        {
            try
            {
                con = await DBConnection.ApexConnection();
                cmd = new DBConnection().BuildProcedureCommand("P_DELETE_WORKHISTORY", con);
                cmd.Parameters.AddWithValue("@EmpId", EmpId);
                cmd.Parameters.AddWithValue("@id", id);
               
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    bool result=false;
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0).Equals(1) ? true : false;
                    }
                    return result;
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

    }
}