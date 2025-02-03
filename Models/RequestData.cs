using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCMSAutomationAPI.Models
{
    public class RequestData
    {
        public string tracknum { get; set; }
        public string branch_name { get; set; }
        public string technician { get; set; }

        public string complainant_name { get; set; }
        public string First_name_petitioner { get; set; }
        public string Middle_name_petitioner { get; set; }
        public string Last_name_pet { get; set; }
        public string acct_num { get; set; }
        public string acct_type { get; set; }
        public string acct_ccy { get; set; }
        public string addr_1_complainant { get; set; }
        public string addr_2_complainant { get; set; }
        public string acct_owner_city { get; set; }
        public string acct_owner_state { get; set; }
        public string acct_owner_country { get; set; }
        public string acct_owner_pcode { get; set; }
        public string acct_owner_phnum { get; set; }
        public string acct_owner_office_phnum { get; set; }
        //public string comp_channel { get; set; }
        public string complainant_location { get; set; }
        public string complainant_email { get; set; }
        public string complainant_channel { get; set; }
        public string complainant_fininmpl { get; set; }
        public string complainant_category { get; set; }
        public string complainant_subcat { get; set; }
        public string comp_subj { get; set; }
        public string comp_desc { get; set; }
        public string comp_prayer { get; set; }
        public string comp_date_recv { get; set; }
        public string comp_date_clsed { get; set; }
        public string amt_applicable { get; set; }
        public string amt_refunde { get; set; }
        public string amt_recovd { get; set; }
        public string action_taken { get; set; }
        public string Status { get; set; }
        public string bank_remark { get; set; }
        public string Root_cause { get; set; }
        public string Preferred_contact_phone { get; set; }
        public string Preferred_contact_Email { get; set; }
        public string Preferred_contact_address { get; set; }
        public string UniqueIdentificationNumber { get; set; }
       
        public string client_type { get; set; }
        public string comp_age { get; set; }
        public string comp_gender { get; set; }
    }
    public class ResponseData
    {
        public string TransactionRmk { get; set; }
        public string TrackNum { get; set; }

        public bool status { get; set; }
    }

}