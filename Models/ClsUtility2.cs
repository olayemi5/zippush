using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace CCMSAutomationAPI.Models
{
    public class ClsUtility2
    {
       // String myStatementCnn = ConfigurationManager.ConnectionStrings["CCMSConn"].ConnectionString;
        //ClsUtility2 utility = new ClsUtility2();
        public ResponseData ValidatePayload(RequestData ReqData)
        {
            ResponseData respData = new ResponseData();
            if (ReqData.tracknum == "" || ReqData.tracknum == null)
            {
                respData.TransactionRmk = "Invalid Track Number.";
                respData.status = false;
                respData.TrackNum = ReqData.tracknum;
                rLogTracker("Insert Notifications: ", "Invalid Track Number.");
                return respData;
            }
            else if (ReqData.acct_ccy == "" || ReqData.acct_ccy == null)
            {
                respData.TransactionRmk = "Invalid Account Currency.";
                respData.TrackNum = ReqData.acct_ccy;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Account Currency.");
                return respData;
            }
            else if (ReqData.acct_num == "" || ReqData.acct_num == null)
            {
                respData.TransactionRmk = "Invalid Account Number.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Account Number.");
                return respData;
            }
            else if (ReqData.acct_owner_office_phnum == "" || ReqData.acct_owner_office_phnum == null)
            {
                respData.TransactionRmk = "Invalid Acct Owner Office Phone Number.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Acct Owner Office Phone Number.");
                return respData;
            }
            else if (ReqData.acct_type == "" || ReqData.acct_type == null)
            {
                respData.TransactionRmk = "Invalid Account Type.";
                respData.TrackNum = ReqData.tracknum;
                rLogTracker("Insert Notifications: ", "Invalid Account Type.");
                respData.status = false;
                return respData;
            }
            else if (ReqData.amt_applicable == "" || ReqData.amt_applicable == null)
            {
                respData.TransactionRmk = "Invalid Amount Applicable.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Amount Applicable.");
                return respData;
            }
            else if (ReqData.complainant_channel == "" || ReqData.complainant_channel == null)
            {
                respData.TransactionRmk = "Invalid Complaint Channel.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Complaint Channel.");
                return respData;
            }
            else if (ReqData.complainant_name == "" || ReqData.complainant_name == null)
            {
                respData.TransactionRmk = "Invalid Complaint Name.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Complaint Name.");
                return respData;
            }
            else if (ReqData.First_name_petitioner == "" || ReqData.First_name_petitioner == null)
            {
                respData.TransactionRmk = "Invalid First Name's Petitioner.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid First Name's Petitioner.");
                return respData;
            }
            //else if (ReqData.Middle_name_petitioner == "" || ReqData.Middle_name_petitioner == null)
            //{
            //    respData.TransactionRmk = "Invalid Middle Name Petitioner.";
            //    respData.TrackNum = ReqData.tracknum;
            //    respData.status = false;
            //    rLogTracker("Insert Notifications: ", "Invalid Middle Name Petitioner.");
            //    return respData;
            //}
            else if (ReqData.Last_name_pet == "" || ReqData.Last_name_pet == null)
            {
                respData.TransactionRmk = "Invalid Last Name of Petitioner.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Last Name of Petitioner.");
                return respData;
            }
            else if (ReqData.Preferred_contact_address == "" || ReqData.Preferred_contact_address == null)
            {
                respData.TransactionRmk = "Invalid Preferred Contact Address.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Preferred Contact Address.");
                return respData;
            }
            else if (ReqData.Preferred_contact_address == "" || ReqData.Preferred_contact_address == null)
            {
                respData.TransactionRmk = "Invalid Preferred Contact Address.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Preferred Contact Address.");
                return respData;
            }
            else if (ReqData.Preferred_contact_phone == "" || ReqData.Preferred_contact_phone == null)
            {
                respData.TransactionRmk = "Invalid Preferred Contact Phone.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Preferred Contact Phone.");
                return respData;
            }
            else if (ReqData.Root_cause == "" || ReqData.Root_cause == null)
            {
                respData.TransactionRmk = "Invalid Root_cause.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Root cause.");
                return respData;
            }
            else if (ReqData.branch_name == "" || ReqData.branch_name == null)
            {
                respData.TransactionRmk = "Invalid Branch Name.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Branch Name.");
                return respData;
            }
            else if (ReqData.Status == "" || ReqData.Status == null)
            {
                respData.TransactionRmk = "Invalid Status.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Status.");
                return respData;
            }
            else if (ReqData.comp_age == "" || ReqData.comp_age == null)
            {
                respData.TransactionRmk = "Invalid Age.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Age.");
                return respData;
            }
            else if (ReqData.comp_gender == "" || ReqData.comp_gender == null)
            {
                respData.TransactionRmk = "Invalid Gender.";
                respData.TrackNum = ReqData.tracknum;
                respData.status = false;
                rLogTracker("Insert Notifications: ", "Invalid Gender.");
                return respData;
            }
            else
            {
                respData.TransactionRmk = ReqData.tracknum;
                respData.TrackNum = ReqData.tracknum;
                respData.status = true;
                return respData;
            }
        }
        public void rLogTracker(String nErrorLocation, String nErrDescr)
        {
            string FileName = "ErrorLog-" + (DateTime.Now.ToString("yyyy-MM-dd")) + ".txt";
            String FilePathGen = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog\\" + FileName;

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(nErrorLocation + ":" + nErrDescr + Environment.NewLine + "StackTrace Date :" + DateTime.Now.ToString());

                    writer.WriteLine(Environment.NewLine + "--------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
            catch
            {
                Exception ex = new Exception();
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(nErrorLocation + ":" + nErrDescr + Environment.NewLine + "StackTrace Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "--------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
        }

        public void rTransLog(string TransName, String nTranDetails)
        {
            String fNames = (DateTime.Now.ToString("yyyy-MM-dd")) + ".txt";
            String FilePathGen = AppDomain.CurrentDomain.BaseDirectory + "TransTracker\\TransLog-" + fNames;

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(nTranDetails + "|" + DateTime.Now.ToString() + Environment.NewLine);

                }

            }
            catch
            {
                Exception ex = new Exception();
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(ex.Message + "|" + nTranDetails + DateTime.Now.ToString() + Environment.NewLine);
                }
            }
        }


        public void rLogCard2RollBack(string TransName, String nTranDetails)
        {
            string FileName = ("Card2RollBack.txt");
            String FilePathGen = AppDomain.CurrentDomain.BaseDirectory + "TransTracker\\" + FileName;

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(nTranDetails + "|" + DateTime.Now.ToString());
                }

            }
            catch
            {
                Exception ex = new Exception();
                using (StreamWriter writer = new StreamWriter(FilePathGen, true))
                {
                    writer.WriteLine(ex.Message + "|" + nTranDetails + DateTime.Now.ToString());
                }
            }
        }








        public String GenerateSerial(String StrUniqueID)
        {

            //ClsLibrary oLibrary = new ClsLibrary();
            try
            {
                //return StrUniqueID + DateTime.Now.ToString("yyyymmddhhmmsstt");
                return StrUniqueID + RandomNumber();
            }
            catch (Exception ex)
            {
                rLogTracker(ex.Source + "|" + ex.StackTrace, ex.Message);
                return StrUniqueID + "000111";
            }
        }


        public string RandomNumber()
        {
            try
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int rand = _rdm.Next(_min, _max);
                return rand.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
            /*try5678
            {
                string Conc = "";
                String[] chDat = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "S", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                //return chDat[6];
                String[] numDat = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Random GenPos = new Random();
                Random Gen = new Random();
                int ChrPos = GenPos.Next(1, DigitLen);
                for (int cnt = 1; cnt <= DigitLen; cnt++)
                {

                    int vCha = Gen.Next(0, 25);
                    if (cnt == ChrPos)
                    {
                        Conc += chDat[vCha];
                        cnt++;
                    }


                    int vNum = Gen.Next(0, 9);
                    Conc += numDat[vNum];
                }
                return Conc;
            }
            catch { Exception ex = new Exception(); return null; }*/
        }
               

        public String Serialize(object Dat)
        {
            try
            {
                return JsonConvert.SerializeObject(Dat);
            }
            catch (Exception ex)
            {
                String Er = "Unable to serialize sent object | " + ex.Message;

                return Er;
            }
        }
        public class ResponseCode
        {
            public String CustomerName { get; set; }
            public String AccountBalance { get; set; }
            public String ResidentialAddress { get; set; }
            public String UserStatus { get; set; }
        }


    }
}


//------------------------------------End of Methods-------------------------------

