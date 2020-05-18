using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using MyLogger;
using QVnextDemoBuilder;

namespace QlikSenseEmailAdmin
{
    public partial class frmConfig : Form
    {
        //private QlikSenseJSONHelper qs;
        private readonly QlikSenseAlertRegistry QSReg = new QlikSenseAlertRegistry();

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
            var tooltip = new ToolTip();
            tooltip.SetToolTip(tb_CustomProperty,
                "Create a custom property in QMC, Populate it with emails" + Environment.NewLine +
                "then assign one or more email(s) to individual tasks using this property.");
            tooltip.SetToolTip(label15,
                "Create a custom property in QMC, Populate it with emails" + Environment.NewLine +
                "then assign one or more email(s) to individual tasks using this property.");

            tooltip.SetToolTip(tb_emailto,
                "Default email address to send alerts if a custom alert property is not" + Environment.NewLine +
                "setup or populated for an individual task");
            tooltip.SetToolTip(label8,
                "Default email address to send alerts if a custom alert property is not" + Environment.NewLine +
                "setup or populated for an individual task");

            var proxy = QSReg.GetQmcServer();

            var emailServer = QSReg.GetSmtpServer();
            var emailSsl = QSReg.GetSsl();
            var emailUser = QSReg.GetUsername();
            var emailPort = QSReg.GetPort();
            var emailPw = QSReg.GetPassword();
            var emailFrom = QSReg.GetEmailFrom();
            var emailTo = QSReg.GetEmailTo();
            var emailProperty = QSReg.GetEmailPropertyName();
            var qsWait = QSReg.GetWait();

            tb_qsurl.Text = proxy;
            tb_server.Text = emailServer;
            cb_ssl.Checked = emailSsl == "Y";
            tb_username.Text = emailUser;
            tb_port.Text = emailPort;
            tb_pasword.Text = emailPw;
            tb_emailfrom.Text = emailFrom;
            tb_emailto.Text = emailTo;
            tb_wait.Text = qsWait;
            tb_CustomProperty.Text = emailProperty;

        }

        private void button_testurl_Click(object sender, EventArgs e)
        {
            try
            {
                var logger = new Logger();
                logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                             "\\QlikSenseEmailAdmin\\history");
                logger.SetLogLevel(LogLevel.Debug);

                var qs = new QlikSenseJSONHelper(tb_qsurl.Text.Trim(), Convert.ToInt32(tb_wait.Text));

                logger.Log(LogLevel.Information, "Looking for Failed Tasks...");


                var taskList = qs.GetTaskByStatus(QsTaskStatus.Failed);

                if (taskList.Count > 0) MessageBox.Show("QMC Connetion Success!");
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
                var Emailresult = SendTestEmail();

                MessageBox.Show("Test Email sent successfully!");
            }

            catch (Exception Err)
            {
                MessageBox.Show("Email Failed : " + Err.Message + " Details: " + Err.InnerException);
            }
        }

        private bool SendTestEmail()
        {
            var message = new MailMessage();

            message.To.Add(tb_emailto.Text.Trim());
            message.Subject = "Qliksense Email Alert: TEST !! ";

            message.From = new MailAddress(tb_emailfrom.Text);
            message.IsBodyHtml = false;
            message.Body = "This is a test email form QlikSense Task Alert tool.";

            var smtp = new SmtpClient(tb_server.Text, Convert.ToInt32(tb_port.Text))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(tb_username.Text.Trim(), tb_pasword.Text.Trim()),
                EnableSsl = cb_ssl.Checked
            };

            smtp.Send(message);

            return true;
        }

        private void SetScheduler_Click(object sender, EventArgs e)
        {
            var exePath = Assembly.GetExecutingAssembly().Location;


            // Get the service on the local machine
            using (var ts = new TaskService())
            {
                // Create a new task definition and assign properties

                var td = ts.NewTask();

                td.RegistrationInfo.Description = "Checks Qliksense Task Failures";
                
                // Add a trigger that will fire the task at this time every other day
                var dt = td.Triggers.Add(new DailyTrigger());
                td.Settings.StartWhenAvailable = true;
                dt.Repetition.Duration = TimeSpan.FromDays(1);
                dt.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToInt32(tb_interval.Text));


                // Create an action that will launch app with -a parameter
                td.Actions.Add(new ExecAction(exePath, "-a"));

                // Register the task in the root folder
                try
                {
                    ts.RootFolder.RegisterTaskDefinition(@"QlikSense Email Alert - (Auto)", td,
                        TaskCreation.CreateOrUpdate, tb_taskuser.Text.Trim(), tb_taskpassword.Text.Trim(),
                        TaskLogonType.Password);
                    MessageBox.Show(
                        "Following windows scheduler task is created/updated successfully: \r\n  QlikSense Email Alert - (Auto) ");
                }
                catch (Exception Err)
                {
                    MessageBox.Show("Failed to create the following task: QlikSense Email Alert - (Auto) \r\n" +
                                    Err.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var ts = new TaskService())
            {
                ts.RootFolder.DeleteTask("QlikSense Email Alert - (Auto)");
                MessageBox.Show("QlikSense Email Alert - (Auto) Scheduler task is deleted successfully!");
            }
        }

        private void ResetSendHistory_Click(object sender, EventArgs e)
        {
            var LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\QlikSenseEmailAdmin\\log.txt";


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
            var LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\QlikSenseEmailAdmin\\log.txt";


            try
            {
                Process.Start("notepad.exe", LogFile);
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ViewLog_Click(object sender, EventArgs e)
        {
            var LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\QlikSenseEmailAdmin\\history";
            Process.Start(LogFile);
        }

        private void DeleteUsers_Click(object sender, EventArgs e)
        {
            QRSNTLMWebClient qrsClient;
            qrsClient = new QRSNTLMWebClient("https://usrem-nak002.qliktech.com", 60000);
            var QSReg = new QlikSenseAlertRegistry();
            var logger = new Logger();
            logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                         "\\QlikSenseEmailAdmin\\history");
            logger.SetLogLevel(LogLevel.Debug);
            var qs = new QlikSenseJSONHelper(QSReg.GetQmcServer(), 60000);
            var UserList = qs.GetDeletedUserList(QsTaskStatus.Failed);
            qrsClient.Delete("/qrs/User/" + UserList[0].id);
        }

        private enum TaskStatus
        {
            NeverStarted,
            Triggered,
            Started,
            Queued,
            AbortInitiated,
            Aborting,
            Aborted,
            Success,
            Failed,
            Skipped,
            Retrying,
            Error,
            Reset
        }
    }
}