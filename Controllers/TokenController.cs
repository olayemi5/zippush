using CCMSAutomationAPI.Models;
//using CCMSService;
//using CCMSService.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Jwt.Filters;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;

using System.Net.Http.Headers;
using ClsUtility2 = CCMSAutomationAPI.Models.ClsUtility2;

namespace CCMSAutomationAPI.Controllers
{
    public class TokenController : ApiController
    {
        //static String secretKey = "MF57bzAKFAnq3C3ZpL5OAIvSeePR7OKSjlXTI1vn9x/RDsjkGdDwLdx4MkFc5WNYYrn0L5L89nFStQJyDR4aaQ==";//CreateSecretKey();
        private static string secretKey = "";
        // private static string secretKey = null;

        [System.Web.Http.AllowAnonymous]
        
        [HttpPost, ActionName("gettoken")]
        [HttpHead]
        public TokenResponse GetToken([FromBody] TokenRequestData ReqData)
        {
            CredentialVaultDB vault = new CredentialVaultDB();
            TokenResponse tokenResponse = new TokenResponse();
            string authCode = "";
            string reqIP = "";
            try
            {
                //    authCode = this.Request.Headers.Authorization.Parameter;
                    reqIP = this.Request.Headers.Host.ToString();
                    secretKey = ReqData.SecretKey;
                //    Console.WriteLine(authCode);
                //}
                //catch (Exception ex)
                //{
                //    new ClsUtility2().rLogTracker("Exception GetToken: ", "Authentication Error/Unauthorised Access");
                //    tokenResponse.TransRmk = "Authentication Error/Unauthorised Access";
                //    tokenResponse.TransStatus = false;
                //    tokenResponse.AccessToken = null;
                //    return tokenResponse;
                //}
                //if (!AuthenticateUser(authCode))
                //{
                //    tokenResponse.TransRmk = "Authentication Error/Unauthorised Access";
                //    tokenResponse.TransStatus = false;
                //    tokenResponse.AccessToken = null;
                //    new ClsUtility2().rLogTracker("Exception GetToken: ", "Authenication Error/Unauthorised Access");               
                //    return tokenResponse;
                //}
                /*else*/
                if (!AgentAuthenticator(ReqData.ClientID))
                {
                    tokenResponse.TransRmk = "Unauthorized AgentID Detected. Kindly contact the administrator.";
                    tokenResponse.TransStatus = false;
                    tokenResponse.AccessToken = null;
                    new ClsUtility2().rLogTracker("Exception GetToken: ", "Authenication Error/Unauthorised Access");

                    return tokenResponse;
                }
                else
                {
                    string accessToken = JwtManager.GenerateToken(ReqData.ClientID, secretKey);
                    tokenResponse.TransRmk = "Access Token Generated Successfully.";
                    tokenResponse.TransStatus = true;
                    tokenResponse.AccessToken = accessToken;
                    new ClsUtility2().rLogTracker("GetToken Notifications: ", "Access token generated successfully.");

                    return tokenResponse;
                }
            }
            catch (Exception ex)
            {
                new ClsUtility2().rLogTracker("Exception GetToken: ", "Authentication Error/Unauthorised Access");
                tokenResponse.TransRmk = "Authentication Error/Unauthorised Access";
                tokenResponse.TransStatus = false;
                tokenResponse.AccessToken = null;
                return tokenResponse;
            }
        
        }
            //[JwtAuthentication]
            //[HttpGet, ActionName("get")]
            //[HttpHead]
            //public string Get()
            //{
            //    return "value";
            //}

            private bool CheckAgent(string clientId, string ClientSecretKey)
        {
            bool Isexist = false;
            Isexist = IsAgentExist(clientId);
            return Isexist;
        }
        private bool AuthenticateUser(String Auth)
        {
            try
            {
                String WordPass = Encoding.UTF8.GetString(Convert.FromBase64String(Auth));
                String[] Cre = WordPass.Split(':');
                String nUser = Cre[0].Trim();
                String nPass = Cre[1].Trim();
                if (IsValidUserKey(nUser, nPass))
                { return true; }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                //ExceptionLogging.SendExcepToDB(ex, "Empty", "Mthd:AuthenticateUser|class: TokenController");
                new ClsUtility2().rLogTracker("Exception AuthenticateUser: ", ex.ToString());
                return false;
            }
        }
        private bool IsValidUserKey(string oUser, string oPass)
        {
            try
            {
                String myUser = WebConfigurationManager.AppSettings["callUser"];
                String myPass = WebConfigurationManager.AppSettings["callPass"];
                myPass = Encoding.UTF8.GetString(Convert.FromBase64String(myPass));
                myUser = Encoding.UTF8.GetString(Convert.FromBase64String(myUser));

                if (myUser == oUser && myPass == oPass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
               // ExceptionLogging.SendExcepToDB(ex, "Empty", "Mthd:IsValidUserKey|class: TokenController");
                new ClsUtility2().rLogTracker("Exception IsValidUserKey: ", ex.ToString());
                return false;
            }
        }
       
        private bool AgentAuthenticator(string clientId)
        {
            
            String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();
            SqlConnection conn = null;
            bool status = false;
            try
            {
             {
            string SqlText = "Proc_AgentSetupDetails";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(SqlText, connection);
                connection.Open();

                cmd.CommandText = "Proc_AgentSetupDetails";

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClientId", clientId);
                    try
                    {   
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                        string _secretKey = dr["ClientSecretKey"].ToString();
                        if(secretKey == _secretKey)
                           status = true;
                        else
                           status = false;
                        }
                    dr.Close();
                    return status;
                }
                catch (Exception e)
                {
                    new ClsUtility2().rLogTracker("Exception AgentAuthenticator: ", e.ToString());
                }
                connection.Close();

                return status;
              }
             }
       
               }
            catch (Exception e)
            {
                //ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:GetSecretKey|class: JwtManager");
                new ClsUtility2().rLogTracker("Exception GetSecretKey: ", e.ToString());
                return status;
            }
           
        }
        private bool IsAgentExist(string clientId)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            bool exist = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Proc_AgentSetupDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 40).Value = clientId;
                cmd.Connection = con;

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    exist = true;
                }
                Console.WriteLine("Agent exists.");

                con.Close();
                return exist;
            }
            catch (Exception e)
            {
                //ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:IsAgentExist|class: TokenController");
                new ClsUtility2().rLogTracker("Exception IsAgentExist: ", e.ToString());
            }
           
            return false;
        }
    }
}