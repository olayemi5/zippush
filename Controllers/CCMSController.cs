using CCMSAutomationAPI.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
//using CCMSService;
//using CCMSService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Jwt.Filters;
using ClsUtility2 = CCMSAutomationAPI.Models.ClsUtility2;

namespace CCMSAutomationAPI.Controllers
{
    public class CCMSController : ApiController
    {
        ResponseData respData = new ResponseData();
        ClsUtility2 utility = new ClsUtility2();
        [HttpGet, ActionName("echo")]
        public String echo()
        {
            return "Welcome to Customer Complaints Management Systems API Hub";
        }

        [JwtAuthentication]
        [HttpPost, ActionName("insertrequest")]
        [HttpHead]
        public ResponseData InsertRequest([FromBody] RequestData ReqData)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();
            // string SqlText = "Proc_ccms_comp_CRM360";
            string SqlText = "Proc_ccms_comp_CRM360";

            utility.rLogTracker("Request before Payload Validation With trackNum :" + ReqData.tracknum + " And Request payload: ",  JsonConvert.SerializeObject(ReqData));

            ResponseData respData =  utility.ValidatePayload(ReqData);

            utility.rLogTracker("Request afetr Payload Validation With trackNum :" + ReqData.tracknum + " And Request payload: ", JsonConvert.SerializeObject(ReqData));

            if (!respData.status)
            {
                utility.rLogTracker("Response from the validatePayload function: ", JsonConvert.SerializeObject(respData));
                return respData;
            }
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                bool status = IsServerConnected(connectionString);

                if (!status)
                {
                    respData.TransactionRmk = "Connection to database failed.";                   
                    utility.rLogTracker("Insert Notifications: ", "For trackNum Connection to database failed.");
                    respData.TrackNum = "";
                    respData.status =false;
                    return respData;
                }
                SqlCommand cmd = new SqlCommand(SqlText, con);               
                {
                    con.Open();
                    cmd.CommandText = SqlText;
                    utility.rLogTracker("SqlCommand: ", "1");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tracknum", ReqData.tracknum);
                    cmd.Parameters.AddWithValue("@branch_name", ReqData.branch_name);
                    cmd.Parameters.AddWithValue("@technician", ReqData.technician);
                    cmd.Parameters.AddWithValue("@client_type", ReqData.client_type);
                    cmd.Parameters.AddWithValue("@comp_name", ReqData.complainant_name);
                    cmd.Parameters.AddWithValue("@First_name_pet", ReqData.First_name_petitioner);
                    cmd.Parameters.AddWithValue("@Middle_name_pet", ReqData.Middle_name_petitioner);
                    cmd.Parameters.AddWithValue("@Last_name_pet", ReqData.Last_name_pet);
                    cmd.Parameters.AddWithValue("@acct_num", ReqData.acct_num);
                    cmd.Parameters.AddWithValue("@acct_type", ReqData.acct_type);
                    cmd.Parameters.AddWithValue("@acct_ccy", ReqData.acct_ccy);
                    cmd.Parameters.AddWithValue("@addr_1_comp", ReqData.addr_1_complainant);
                    cmd.Parameters.AddWithValue("@addr_2_comp", ReqData.addr_2_complainant);
                    cmd.Parameters.AddWithValue("@acct_owner_city", ReqData.acct_owner_city);
                    cmd.Parameters.AddWithValue("@acct_owner_state", ReqData.acct_owner_state);
                    cmd.Parameters.AddWithValue("@acct_owner_country", ReqData.acct_owner_country);
                    cmd.Parameters.AddWithValue("@acct_owner_pcode", ReqData.acct_owner_pcode);
                    cmd.Parameters.AddWithValue("@acct_owner_phnum", ReqData.acct_owner_phnum);
                    cmd.Parameters.AddWithValue("@acct_owner_offphnum", ReqData.acct_owner_office_phnum);
                    cmd.Parameters.AddWithValue("@comp_channel", ReqData.complainant_channel);
                    cmd.Parameters.AddWithValue("@comp_location", ReqData.complainant_location);
                    cmd.Parameters.AddWithValue("@comp_email", ReqData.complainant_email);
                    cmd.Parameters.AddWithValue("@comp_fininmpl", ReqData.complainant_fininmpl);
                    cmd.Parameters.AddWithValue("@comp_cat", ReqData.complainant_category);
                    cmd.Parameters.AddWithValue("@comp_subcat", ReqData.complainant_subcat);
                    cmd.Parameters.AddWithValue("@comp_subj", ReqData.comp_subj);
                    cmd.Parameters.AddWithValue("@comp_desc", ReqData.comp_desc);
                    cmd.Parameters.AddWithValue("@comp_prayer", ReqData.comp_prayer);
                    cmd.Parameters.AddWithValue("@comp_date_recv", ReqData.comp_date_recv);
                    cmd.Parameters.AddWithValue("@comp_date_clsed", ReqData.comp_date_clsed);
                    cmd.Parameters.AddWithValue("@amt_applicable", ReqData.amt_applicable);
                    cmd.Parameters.AddWithValue("@amt_refunde", ReqData.amt_refunde);
                    cmd.Parameters.AddWithValue("@amt_recovd", ReqData.amt_recovd);
                    cmd.Parameters.AddWithValue("@action_taken", ReqData.action_taken);
                    cmd.Parameters.AddWithValue("@Status", ReqData.Status);
                    cmd.Parameters.AddWithValue("@bank_remark", ReqData.bank_remark);
                    cmd.Parameters.AddWithValue("@Root_cause", ReqData.Root_cause);
                    cmd.Parameters.AddWithValue("@Preferred_contact_phone", ReqData.Preferred_contact_phone);
                    cmd.Parameters.AddWithValue("@Preferred_contact_Email", ReqData.Preferred_contact_Email);
                    cmd.Parameters.AddWithValue("@Preferred_contact_address", ReqData.Preferred_contact_address);
                    cmd.Parameters.AddWithValue("@UniqueIdentificationNumber", ReqData.UniqueIdentificationNumber);
                    cmd.Parameters.AddWithValue("@comp_age", ReqData.comp_age);
                    cmd.Parameters.AddWithValue("@comp_gender", ReqData.comp_gender);

                    var returnTrackNum = cmd.Parameters.Add("@ReturnVal", SqlDbType.VarChar);
                    returnTrackNum.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    var trackNum = returnTrackNum.Value;
                    // if ((int)trackNum == 0)
                    //{
                    //    respData.TransactionRmk = "Duplicate to tracking number";
                    //    respData.TrackNum = ReqData.tracknum;
                    //    utility.rLogTracker("Insert Failure Notifications: ", "Track Number: " + respData.TrackNum + " Failed to Insert into Database.");
                    //    respData.status = false;
                    //    con.Close();
                    //    return respData;
                    //}
                    if((int)trackNum == 1)
                    {
                        respData.TransactionRmk = "Customer Complaints Record Inserted Successfully.";
                        respData.TrackNum = ReqData.tracknum;
                        respData.status = true;
                        utility.rLogTracker("Insert Notifications: ", "Track Number: "+ ReqData.tracknum + " Successfully Inserted.");

                        con.Close();
                        return respData;
                    }
                    //else if((int)trackNum == 0)
                    //{
                    //    respData.TransactionRmk = "Duplicate to tracking number";
                    //    respData.TrackNum = ReqData.tracknum;
                    //    utility.rLogTracker("Insert Failure Notifications: ", "Track Number: " + ReqData.tracknum + " Failed to Insert into Database.");
                    //    respData.status = false;
                    //    con.Close();
                    //    return respData;
                    //}
                    else
                    {
                        respData.TransactionRmk = "Failed to Insert into Database.";
                        respData.TrackNum = ReqData.tracknum;
                        utility.rLogTracker("Insert Failure Notifications: ", "Track Number: " + ReqData.tracknum + " Failed to Insert into Database.");
                        respData.status = false;
                        con.Close();
                        return respData;
                    }
                }
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendExcepToDB(ex, "Empty", "Mthd:ProcessXMLRequest|class: XMLDataDAO");
                utility.rLogTracker("Exception InsertRequest StackMe:: ", ex.Message);
                respData.TransactionRmk = ex.Message;
                respData.status = false;
                respData.TrackNum = ReqData.tracknum;
                return respData;
            }
        }
        private static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }      
       
    }
}
