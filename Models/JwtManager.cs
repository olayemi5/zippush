//using CCMSService.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ClsUtility2 = CCMSAutomationAPI.Models.ClsUtility2;

namespace CCMSAutomationAPI.Models
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        //private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        public static string GenerateToken(string clientId,string secretKey, int expireMinutes = 1)
        {
            try
            {
                var symmetricKey = Convert.FromBase64String(secretKey);
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject= new ClaimsIdentity(new[]
                            {
                            new Claim(ClaimTypes.Name, clientId)
                        }),
                    

                    Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                    

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {
              //  ExceptionLogging.SendExcepToDB(ex, "Empty", "Mthd:GenerateToken|class: JwtManager");
                new ClsUtility2().rLogTracker("Exception GenerateToken: ", ex.ToString());
            }
            return null;
        }

        public static ClaimsPrincipal GetPrincipal(string token,string clientId)
        {
            string secretKey = JwtManager.GetSecretKey(clientId);
            if (secretKey != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                    if (jwtToken == null)
                        return null;

                    var symmetricKey = Convert.FromBase64String(secretKey);

                    var validationParameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                    };

                    var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                    return principal;
                }
                catch (Exception ex)
                {
                   // ExceptionLogging.SendExcepToDB(ex, "Empty", "Mthd:GetPrincipal|class: JwtManager");
                    new ClsUtility2().rLogTracker("Exception GetPrincipal: ", ex.ToString());
                    return null;
                }
            }
            return null;
        }
        public static string GetSecretKey(string clientId)
        {
            string secretKey = null;
            SqlConnection conn = null;
            //try { 
            //  String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();

            //      using (conn = new SqlConnection(connectionString))

            //      conn.Open();
            //      using (SqlCommand cmd = conn.CreateCommand())
            //      {
            //          cmd.CommandText = "Proc_AgentSetupDetails";
            //          cmd.CommandType = CommandType.StoredProcedure;
            //          cmd.Parameters.AddWithValue("@ClientId", null);
            //          //cmd.Parameters.AddWithValue("@ClientSecretKey", null);

            //          SqlDataReader reader = cmd.ExecuteReader();
            //          if (reader.Read())
            //          {
            //              secretKey = reader["ClientSecretKey"].ToString();
            //          }

            //          cmd.ExecuteNonQuery();
            //          return secretKey;
            //      }                   
            //  }
            //  catch(Exception e)
            //  {
            //      ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:GetSecretKey|class: JwtManager");
            //      new CCMSService.ClsUtility().rLogTracker("Exception GetSecretKey: ", e.ToString());
            //      return null;
            //  }
         
                    string SqlText = "Proc_AgentSetupDetails";
                    String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlDataReader dr;
                        SqlCommand cmd = new SqlCommand(SqlText, connection);
                        connection.Open();

                        cmd.CommandText = "Proc_AgentSetupDetails";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientId", /*DBNull.Value*/clientId);
                        try
                        {
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                string _secretKey = dr["ClientSecretKey"].ToString();
                                secretKey = _secretKey;
                            }
                            dr.Close();
                            return secretKey;
                        }
                        catch (Exception e)
                        {
                            //ExceptionLogging.SendExcepToDB(e, "Empty", "Mthd:GetSecretKey|class: JwtManager");
                            new ClsUtility2().rLogTracker("Exception GetSecretKey|class: JwtManager ", e.ToString());
                        }
                        connection.Close();

                        return secretKey;
                    }
                }
              

            
    }
}