using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CyberArk.AIM.NetPasswordSDK;
using CyberArk.AIM.NetPasswordSDK.Exceptions;
using System.Web.Configuration;
using ClsUtility2 = CCMSAutomationAPI.Models.ClsUtility2;

namespace CCMSAutomationAPI.Models
{
    public class CredentialVaultDB
    {
        public CredentialVaultDB() {
            PSDKPassword password;
            CredentialVault secrets = new CredentialVault();
            try { 
            PSDKPasswordRequest passRequest = new PSDKPasswordRequest();

            passRequest.AppID = "BBG";
            passRequest.Safe = "BBG_DataBase Safe";
            passRequest.Folder = "root";
            passRequest.Object = "Database-FCMBMICROSOFTSQLSERVER-10.50.58.55-dev";

            string application = "BBG";

            //try {
            //    application = WebConfigurationManager.AppSettings["SecretKey"];
            //}
            //catch {
            //    Exception ex = new Exception();
            //    //logger.Error(ex)
            //}


            passRequest.Reason = application + " Application connecting to Database";
            passRequest.ConnectionPort = 18923;
            passRequest.ConnectionTimeout = 30;
            passRequest.RequiredProperties.Add("PolicyId");
            passRequest.RequiredProperties.Add("userName");
            passRequest.RequiredProperties.Add("Address");

            password = PasswordSDK.GetPassword(passRequest);
            //' Analyzing the response
            //'1. AppID -RIB Prod
            //'2. ConnectionPort - 18923
            //'3. ConnectionTimeout - 30
            //'4. Safe - RIBProduction Safe
            //'5. Folder - root
            //'6. Object - Database-FCMBMICROSOFTSQLSERVER-10.5.251.138-RIBPUser
            //'7. Reason - BBG Application connecting to Database

            secrets.Password = password.SecureContent.ToString();
            secrets.UserName = password.UserName;
            secrets.IpAddress = password.Address;
                new ClsUtility2().rLogTracker("Exception AuthenticateAsync: " +" "+ secrets.Password + " "+ secrets.UserName+" "+password.Address, "help");
            }
            //' Use password properties
            //'password.UserName
            //'password.Address
            //'password.GetAttribute("PassProps.PolicyId")
        catch 
            {
                Exception ex = new PSDKException("dddddd","ddddd",2,2);
                //    logger.Error(ex)
                new ClsUtility2().rLogTracker("Exception AuthenticateAsync: " + " " + secrets.Password + " " + secrets.UserName + " " + secrets.IpAddress+ex.Message+ex.StackTrace+ex.InnerException, "help");
            }

            finally {
                //    If Not IsNothing(password) Then
                //        password = Nothing
                //    End If
                //    '   
                //End Try
            }


           // return secrets;

        }


    }
}