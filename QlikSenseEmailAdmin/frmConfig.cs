using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using QlikSenseJSONObjects;
using System.Diagnostics;
using MyLogger;
using System.Net.Mail;
using System.Reflection;
using QVnextDemoBuilder;
using Microsoft.Win32.TaskScheduler;

namespace QlikSenseEmailAdmin
{
    public partial class frmConfig : Form
    {

        enum TaskStatus { NeverStarted, Triggered, Started, Queued, AbortInitiated, Aborting, Aborted, Success, Failed, Skipped, Retrying, Error, Reset };

        //private QlikSenseJSONHelper qs;
        private QlikSenseAlertRegistry QSReg = new QlikSenseAlertRegistry();
        public frmConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            QSReg.SetQmcServer(tb_qsurl.Text.Trim());

            QSReg.SetSmptServer(tb_server.Text.Trim());
            
            QSReg.SetSsl(cb_ssl.Checked);

            QSReg.SetUsername(tb_username.Text.Trim());

            QSReg.SetPort(tb_port.Text.Trim());

            QSReg.SetPassword(tb_pasword.Text.Trim());

            QSReg.SetEmailFrom(tb_emailfrom.Text.Trim());

            QSReg.SetEmailTo(tb_emailto.Text.Trim());

            QSReg.SetWait(tb_wait.Text.Trim());

            QSReg.SetEmailPropertyName(tb_CustomProperty.Text.Trim());

            MessageBox.Show("Setting are saved successfully", "QlikSense Email Alert", MessageBoxButtons.OK);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(tb_CustomProperty, "Create a custom property in QMC, Populate it with emails" + System.Environment.NewLine + "then assign one or more email(s) to individual tasks using this property.");
            tooltip.SetToolTip(label15, "Create a custom property in QMC, Populate it with emails" + System.Environment.NewLine + "then assign one or more email(s) to individual tasks using this property.");

            tooltip.SetToolTip(tb_emailto, "Default email address to send alerts if a custom alert property is not" + System.Environment.NewLine + "setup or populated for an individual task");
            tooltip.SetToolTip(label8, "Default email address to send alerts if a custom alert property is not" + System.Environment.NewLine + "setup or populated for an individual task");


            string proxy = "";
            // Int32 timeout = 60 * 1000;
            string task = "";
            // bool synchronous = true;

            string email_server = "";
            string email_port = "";
            string email_user = "";
            string email_pw = "";
            string email_to = "";
            string email_from = "";
            string email_ssl = "N";
            string qs_wait = "500";
            string email_property = "";


            proxy = QSReg.GetQmcServer();

            email_server = QSReg.GetSmtpServer();
            email_ssl = QSReg.GetSsl();
            email_user = QSReg.GetUsername();
            email_port = QSReg.GetPort();
            email_pw = QSReg.GetPassword();
            email_from = QSReg.GetEmailFrom();
            email_to = QSReg.GetEmailTo();
            email_property = QSReg.GetEmailPropertyName();
            qs_wait = QSReg.GetWait();

            string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\log.txt";


            tb_qsurl.Text = proxy;
            tb_server.Text = email_server;
            if(email_ssl == "Y")
                { cb_ssl.Checked = true; }
            else
                { cb_ssl.Checked = false; };
            tb_username.Text = email_user;
            tb_port.Text = email_port;
            tb_pasword.Text = email_pw;
            tb_emailfrom.Text = email_from;
            tb_emailto.Text = email_to;
            tb_wait.Text = qs_wait;
            tb_CustomProperty.Text = email_property;


            string[] logdata = { };
            if (File.Exists(LogFile))
            {
                logdata = File.ReadAllLines(LogFile);
            }



    

        }

        private void button_testurl_Click(object sender, EventArgs e)
        {

            try
            {
                Logger logger = new Logger();
                logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\history");
                logger.SetLogLevel(LogLevel.Debug);



                //QlikSenseJSONHelper qs = new QlikSenseJSONHelper(tb_qsurl.Text.Trim(), 60000, logger);
                QlikSenseJSONHelper qs = new QlikSenseJSONHelper(tb_qsurl.Text.Trim(), Convert.ToInt32(tb_wait.Text)); //http timeout 60 seconds
                                                                                                                               //SMTPMail mail = new SMTPMail();
                                                                                                                               //TODO: - set mail params SMTPServerHost, and SMTPServerPort, AdminEmailAddress, FromEmailAddress,EnvironmentName there will probably be a couple more

                logger.Log(LogLevel.Information, "Looking for Failed Tasks...");
                //TaskStatus

                List<QlikSenseTaskResult> TaskList = (List<QlikSenseTaskResult>)qs.GetTaskByStatus(QsTaskStatus.Failed);

                if (TaskList.Count >= 0)
                {
                    MessageBox.Show("QMC Connetion Success!");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("QMC Connetion Failure! Check URL or the currrent user credentials");
            }
 




        }

        private void button_email_Click(object sender, EventArgs e)
        {

            try
            {
               Boolean Emailresult =  SendTestEmail();

                MessageBox.Show("Test Email sent successfully!");

            }
            
            catch (Exception Err)
            {

                MessageBox.Show("Email Failed : " + Err.Message + " Details: " + Err.InnerException );
            }





        }

        private Boolean SendTestEmail()
        {
            //try
            //{

                MailMessage message = new MailMessage();



                message.To.Add(tb_emailto.Text.Trim());
                message.Subject = "Qliksense Email Alert: TEST !! ";

                message.From = new System.Net.Mail.MailAddress(tb_emailfrom.Text);
                message.IsBodyHtml = false;
                message.Body = "This is a test email form QlikSense Task Alert tool.";

                SmtpClient smtp = new SmtpClient(tb_server.Text, Convert.ToInt32(tb_port.Text));

                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new System.Net.NetworkCredential(tb_username.Text.Trim(), tb_pasword.Text.Trim());

                smtp.EnableSsl = cb_ssl.Checked;


                smtp.Send(message);
                return true;
      
        }

        private void SetScheduler_Click(object sender, EventArgs e)
        {
            string Exepath = Assembly.GetExecutingAssembly().Location;


            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {

                
                // Create a new task definition and assign properties

                TaskDefinition td = ts.NewTask();
                
                td.RegistrationInfo.Description = "Checks Qliksense Task Failures";
                //td.Settings.RunOnlyIfLoggedOn = false;
                // Add a trigger that will fire the task at this time every other day
                DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(1));
                td.Settings.StartWhenAvailable = true;
                dt.Repetition.Duration = TimeSpan.FromDays(1);
                dt.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(tb_interval.Text));

            

                // Create an action that will launch app with -a parameter
                td.Actions.Add(new ExecAction(Exepath, "-a", null));

                // Register the task in the root folder
                try
                {
                    ts.RootFolder.RegisterTaskDefinition(@"QlikSense Email Alert - (Auto)", td, TaskCreation.CreateOrUpdate, tb_taskuser.Text.Trim(), tb_taskpassword.Text.Trim(), TaskLogonType.Password);
                    MessageBox.Show("Following windows scheduler task is created/updated successfully: \r\n  QlikSense Email Alert - (Auto) ");
                }
                catch (Exception Err)
                {
                    MessageBox.Show("Failed to create the following task: QlikSense Email Alert - (Auto) \r\n" + Err.Message );
                }
               
              
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask("QlikSense Email Alert - (Auto)");
                MessageBox.Show("QlikSense Email Alert - (Auto) Scheduler task is deleted successfully!");
            }
        }

        private void ResetSendHistory_Click(object sender, EventArgs e)
        {
            string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\log.txt";


            try
            { 
                File.Delete(LogFile);
                File.Create(LogFile).Close();
                MessageBox.Show("Sent history file keeping the 24 hour notification delay per task is cleared!");
            }


            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }
    



}

        private void ViewSendHistory_Click(object sender, EventArgs e)
        {
            string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\log.txt";


            try
            {

                System.Diagnostics.Process.Start("notepad.exe", LogFile);
            }


            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void ViewLog_Click(object sender, EventArgs e)
        {
            string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\history";
            Process.Start(LogFile);
        }

        private void DeleteUsers_Click(object sender, EventArgs e)
        {
            QRSNTLMWebClient qrsClient;
            qrsClient = new QRSNTLMWebClient("https://usrem-nak002.qliktech.com", 60000);
            QlikSenseAlertRegistry QSReg = new QlikSenseAlertRegistry();
            Logger logger = new Logger();
            logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\history");
            logger.SetLogLevel(LogLevel.Debug);
            QlikSenseJSONHelper qs = new QlikSenseJSONHelper(QSReg.GetQmcServer(), 60000);
            List<QlikSenseUserList> UserList = (List<QlikSenseUserList>)qs.GetDeletedUserList(QsTaskStatus.Failed);
            qrsClient.Delete("/qrs/User/" + UserList[0].id.ToString());
            
        }

        
    }
}
