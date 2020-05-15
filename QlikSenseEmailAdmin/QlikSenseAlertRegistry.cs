using Microsoft.Win32;

namespace QlikSenseEmailAdmin
{
    class QlikSenseAlertRegistry
    {

        // The name of the key must include a valid root.
        const string hklm = "HKEY_LOCAL_MACHINE\\Software\\Qliksense_Email_Alert";
        public string KeyName;


        public string GetQmcServer()
        {
            KeyName = "qs_server";
            var var_temp = (string)Registry.GetValue(hklm,KeyName, "Return this default if NoSuchName does not exist.");           
            return var_temp;
        }

        public string GetSmtpServer()
        {
            KeyName = "smtp_server";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetSsl()
        {
            KeyName = "smtp_ssl";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetUsername()
        {
            KeyName = "smtp_username";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetPort()
        {
            KeyName = "smtp_port";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetEmailFrom()
        {

            KeyName = "smtp_email_from";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetEmailTo()
        {
            KeyName = "smtp_email_to";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetPassword()
        {
            KeyName = "smtp_password";
            var var_temp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return var_temp;
        }

        public string GetWait()
        {
            KeyName = "qs_wait";
            var varTemp = (string)Registry.GetValue(hklm, KeyName, "Return this default if NoSuchName does not exist.");
            return varTemp;
        }

        public string GetEmailPropertyName()
        {
            KeyName = "qs_email_property_name";
            string var_temp = (string)Registry.GetValue(hklm, KeyName, "SendAlertTo");
            return var_temp;
        }

        public void SetQmcServer(string proxyUrl)
        {
            KeyName = "qs_server";
            Registry.SetValue(hklm, KeyName, proxyUrl);
        }

        public void SetSmptServer(string smtpServer)
        {
            KeyName = "smtp_server";
            Registry.SetValue(hklm, KeyName, smtpServer);
        }

        public void SetSsl(bool SslValue)
        {

            KeyName = "smtp_ssl";

            var regValue = SslValue ? "Y" : "N";

            Registry.SetValue(hklm, KeyName, regValue);
        }

        public void SetUsername(string username)
        {
            KeyName = "smtp_username";
            Registry.SetValue(hklm, KeyName, username);
        }

        public void SetPort(string port)
        {
            KeyName = "smtp_port";
            Registry.SetValue(hklm, KeyName, port);
        }

        public void SetEmailFrom(string emailfrom)
        {
            KeyName = "smtp_email_from";
            Registry.SetValue(hklm, KeyName, emailfrom);
        }

        public void SetEmailTo( string emailto)
        {
            KeyName = "smtp_email_to";
            Registry.SetValue(hklm, KeyName, emailto);
        }

        public void SetPassword( string password)
        {
            KeyName = "smtp_password";
            Registry.SetValue(hklm, KeyName, password);
        }

        public void SetWait(string waitTime)
        {
            KeyName = "qs_wait";
            Registry.SetValue(hklm, KeyName, waitTime);
        }

        
        public void SetEmailPropertyName(string PropertyName)
        {
            KeyName = "qs_email_property_name";
            Registry.SetValue(hklm, KeyName, PropertyName);
        }

    }
}
