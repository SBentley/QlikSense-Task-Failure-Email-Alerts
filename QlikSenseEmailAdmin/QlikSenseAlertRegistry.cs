using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace QlikSenseEmailAdmin
{
    class QlikSenseAlertRegistry
    {

        // The name of the key must include a valid root.
        const string hklm = "HKEY_LOCAL_MACHINE\\Software\\Qliksense_Email_Alert";
        //public string subkey ;
         public string keyName ;


        public string GetQMCserver()
        {
            keyName = "qs_server";
            string var_temp = (string)Registry.GetValue(hklm,keyName, "Return this default if NoSuchName does not exist.");           
            return var_temp;
          //  throw new System.NotImplementedException();
        }

        public string GetSMTPserver()
        {

            keyName = "smtp_server";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
            //  throw new System.NotImplementedException();
        }

        public string GetSSL()
        {

            keyName = "smtp_ssl";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
            // throw new System.NotImplementedException();
        }

        public string GetUsername()
        {
            keyName = "smtp_username";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;

            //    throw new System.NotImplementedException();
        }

        public string GetPort()
        {

            keyName = "smtp_port";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
            //   throw new System.NotImplementedException();
        }

        public string GetEmailFrom()
        {

            keyName = "smtp_email_from";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;

            // throw new System.NotImplementedException();
        }

        public string GetEmailTo()
        {
            keyName = "smtp_email_to";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;

            // throw new System.NotImplementedException();
        }

        public string GetPassword()
        {
            keyName = "smtp_password";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
            //  throw new System.NotImplementedException();
        }

        public string GetWait()
        {
            keyName = "qs_wait";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
            //  throw new System.NotImplementedException();
        }

        public string GetEmailPropertyName()
        {
            keyName = "qs_email_property_name";
            string var_temp = (string)Registry.GetValue(hklm, keyName, "SendAlertTo");
            return var_temp;
            //  throw new System.NotImplementedException();
        }



        public void SetQMCserver(string proxyurl)
        {
            keyName = "qs_server";
            Registry.SetValue(hklm, keyName, proxyurl);
            //return var_temp;
        }

        public void SetSMTPserver(string smtpserver)
        {

            keyName = "smtp_server";
            Registry.SetValue(hklm, keyName, smtpserver);
            //  throw new System.NotImplementedException();
        }

        public void SetSSL(Boolean SSLvalue)
        {

            keyName = "smtp_ssl";

            string regvalue;

            if (SSLvalue == true)
            { regvalue = "Y"; }
            else
            { regvalue = "N"; }

            Registry.SetValue(hklm, keyName, regvalue);
        }

        public void SetUsername(string username)
        {
            keyName = "smtp_username";
            Registry.SetValue(hklm, keyName, username);
        }

        public void SetPort(string port)
        {
            keyName = "smtp_port";
            Registry.SetValue(hklm, keyName, port);
        }

        public void SetEmailFrom(string emailfrom)
        {
            keyName = "smtp_email_from";
            Registry.SetValue(hklm, keyName, emailfrom);
        }

        public void SetEmailTo( string emailto)
        {
            keyName = "smtp_email_to";
            Registry.SetValue(hklm, keyName, emailto);
        }

        public void SetPassword( string password)
        {
            keyName = "smtp_password";
            Registry.SetValue(hklm, keyName, password);
        }

        public void SetWait(string waitTime)
        {
            keyName = "qs_wait";
            Registry.SetValue(hklm, keyName, waitTime);
        }

        
        public void SetEmailPropertyName(string PropertyName)
        {
            keyName = "qs_email_property_name";
            Registry.SetValue(hklm, keyName, PropertyName);
        }

    }
}
