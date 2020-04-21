using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using QlikSenseJSONObjects;
using System.Diagnostics;
using MyLogger;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Mime;

namespace QlikSenseEmailAdmin
{

    

    class Program
    {
     //   enum TaskStatus { NeverStarted, Triggered, Started, Queued, AbortInitiated, Aborting, Aborted, Success, Failed, Skipped, Retrying, Error, Reset };

        private QlikSenseJSONHelper qs;

        static void Main(string[] args)
        {


                QlikSenseAlertRegistry QSReg = new QlikSenseAlertRegistry();
        //usage****
        //-proxy:https://usral-msi  -timeout:10000 -task:"test123" -wait
        //***
        Console.WriteLine("Use at your own risk.  Created by Marcus Spitzmiller and Nick Akincilar.");
            Console.WriteLine("");


            //Dim AppPath As String = Exepath.Substring(0, Exepath.LastIndexOf("\"))
            string Exepath = Assembly.GetExecutingAssembly().Location;

            Exepath = Exepath.Replace("\\QlikSenseEmailAdmin.exe", "");

            Logger logger = new Logger();
            logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\history");
            logger.SetLogLevel(LogLevel.Debug);

            /*****************************/
            //this section is where we gather inputs from the command line and config.txt
            /*****************************/

            //TODO: - add SMTPServerHost, and SMTPServerPort, AdminEmailAddress, FromEmailAddress, EnvironmentName there will probably be a couple more


            //defaults
            string proxy = "";

            var jsonData = "";
            var emailServer = "";
            var emailPort = "";
            var emailUser = "";
            var emailPw = "";
            var emailTo = "";
            var emailFrom = "";
            var emailSsl = "N";
            var emailPropertyName = "";


            //******** Delete log files older than 2 days

            var files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\history").GetFiles("*.log");
            foreach (var file in files)
            {
                if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(2))
                {
                    File.Delete(file.FullName);
                }
            }

            //******** Delete log files older than 2 days



            string ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\config.txt";
            string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\QlikSenseEmailAdmin\\log.txt";

            //global settings from config.txt
            string[] global = { };
            if (File.Exists(ConfigFile))
            {
                global = File.ReadAllLines(ConfigFile);
                //args.CopyTo(global, 0);
            }

            string[] logdata = { };
            if (File.Exists(LogFile))
            {
                logdata = File.ReadAllLines(LogFile);
            }

            //  frmConfig frm = new frmConfig();
            // frm.Show();

            if (args.Length == 0)
            {
                Application.Run(new frmConfig());
            }
            else
            {


                if (args[0].Contains(":") == false)   //Get config values from registry unless there is a parameter with ":" in it
                {

                    proxy = QSReg.GetQMCserver();

                    emailServer = QSReg.GetSMTPserver();
                    emailSsl = QSReg.GetSSL();
                    emailUser = QSReg.GetUsername();
                    emailPort = QSReg.GetPort();
                    emailPw = QSReg.GetPassword();
                    emailFrom = QSReg.GetEmailFrom();
                    emailTo = QSReg.GetEmailTo();
                    QSReg.GetWait();
                    emailPropertyName = QSReg.GetEmailPropertyName();

                    goto bypass;
                }

           
                string[] allargs = new string[args.Length];
         
                args.CopyTo(allargs, args.Length-1);



                for (int i = 0; i < allargs.Length; i++)
                {
                    string arg = allargs[i];
                    string[] param = arg.Split(':');

                    switch (param[0])
                    {
                        case "-?":
                        case "/?":
                            Console.WriteLine("Usage:");
                            Console.WriteLine("-proxy:<URL address of proxy>  required example https://server.company.com");
                            //Console.WriteLine(@"-task:<taskname>               required example ""test 123""");
                            //Console.WriteLine("-wait:<# seconds to wait>      optional example 30");
                            Console.WriteLine("   omit -wait to return immediately");
                            Console.WriteLine("   use -wait to wait for the task to finish");
                            Console.WriteLine("     Return Codes:");
                            Console.WriteLine("     0 - task completed successfully");
                            Console.WriteLine("     4 - task timed out");
                            Console.WriteLine("     8 - task failed");
                            Console.WriteLine("");
                            Console.WriteLine("Optionally define any or all parameters in config.txt");
                            //Console.WriteLine("  (to be used globally for all tasks)");
                            Console.WriteLine("");
                            // Console.WriteLine("-debug                         optional");
                            // Console.WriteLine("   omit -wait to return immediately");
                            Console.WriteLine("press any key...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        case "-proxy":
                            proxy = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                proxy += param[j];
                                if (j < param.Length - 1) proxy += ":"; //put back the colon
                            }

                            break;

                        case "-smtp_server":
                            emailServer = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                emailServer += param[j];
                                if (j < param.Length - 1) emailServer += ":"; //put back the colon
                            }


                            break;



                        case "-smtp_port":
                            emailPort = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                emailPort += param[j];
                                if (j < param.Length - 1) emailPort += ":"; //put back the colon
                            }


                            break;

                        case "-stmp_user":
                            emailUser = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                emailUser += param[j];
                                if (j < param.Length - 1) emailUser += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_pw":

                            emailPw = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                emailPw += param[j];
                                if (j < param.Length - 1) emailPw += ":"; //put back the colon
                            }


                            break;

                        case "-smtp_from":

                            emailFrom = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                emailFrom += param[j];
                                if (j < param.Length - 1) emailFrom += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_to":

                            emailTo = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                emailTo += param[j];
                                if (j < param.Length - 1) emailTo += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_enableSSL":

                            emailSsl = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                emailSsl += param[j];
                                if (j < param.Length - 1) emailSsl += ":"; //put back the colon
                            }


                            break;

                        /*case "-wait":
                            timeout = Convert.ToInt32(param[1]);

                            break;
                        case "-task":
                            task = param[1];
                            logger.Log(LogLevel.Information, "Task: " + task);

                            break;*/
                        default:
                            logger.Log(LogLevel.Information, "Unrecognized: " + param[0]);

                            break;

                    }

                }

            bypass:
                logger.Log(LogLevel.Information, "Proxy: " + proxy);
   
                if (proxy == "")
                {
                    logger.Log(LogLevel.Fatal, "Proxy or undefined");

                    Environment.Exit(4);
                }




                var retval = 0;
                try
                {
                    QlikSenseJSONHelper qs = new QlikSenseJSONHelper(proxy, 60000, logger); //http timeout 60 seconds
                                                                                            //SMTPMail mail = new SMTPMail();
                                                                                            //TODO: - set mail params SMTPServerHost, and SMTPServerPort, AdminEmailAddress, FromEmailAddress,EnvironmentName there will probably be a couple more

                    logger.Log(LogLevel.Information, "Looking for Failed Tasks...");
                    //TaskStatus

                    var userList = qs.GetDeletedUserList(QSTaskStatus.Failed);

                    var taskList = qs.GetTaskByStatus(QSTaskStatus.Failed);

                    var taskListError = qs.GetTaskByStatus(QSTaskStatus.Error);

                    var taskListAborted = qs.GetTaskByStatus(QSTaskStatus.Aborted);


                    //TaskList.AddRange(TaskListError);
                    //TaskList.AddRange(TaskListAborted);

                    //***

                    //Nick----8/9/2017
                    var temp = qs.GetTaskByStatusText(QSTaskStatus.Failed);
                    List<TaskResult> myTaskResult = JsonConvert.DeserializeObject<List<TaskResult>>(temp);


                    //temp =  qs.GetTaskByStatusText(QSTaskStatus.Aborted);
                    //List<TaskResult> MyTaskResultAbort = JsonConvert.DeserializeObject<List<TaskResult>>(temp);

                    //MyTaskResult = MyTaskResult.AddRange(MyTaskResultAbort);


                    temp = qs.GetTaskByStatusText(QSTaskStatus.Aborted);
                    jsonData = temp;
                    myTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(jsonData));
                    //List<TaskResult> MyTaskResult = JsonConvert.DeserializeObject<List<TaskResult>>(temp);

                    temp = qs.GetTaskByStatusText(QSTaskStatus.Error);
                    myTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(temp));


                    //****


                    taskList.AddRange(taskListError);
                    taskList.AddRange(taskListAborted);

                    //List<Customproperty> customPropertyList;

                    var alertCount = 0;
                    var ErrorMessage = "";

                    DateTime FileLastEmailDate;

                    logger.Log(LogLevel.Information, "Found " + Convert.ToString(taskList.Count) + " Failed Tasks!");

                    for (int i = 0; i <= taskList.Count - 1; i++)
                    {
                        var ExistingTask = false;
                        var Filecontent = "";

                        var myresult = taskList[i].operational.lastExecutionResult.startTime;


                        var fileRefID = taskList[i].operational.lastExecutionResult.fileReferenceID;

                        myresult = myresult.Replace('T', ' ');
                        myresult = myresult.Replace('Z', ' ');
                        var taskDate = Convert.ToDateTime(myresult);
                        //Convert UTC time to Local Time
                        taskDate = TimeZoneInfo.ConvertTimeFromUtc(taskDate, TimeZoneInfo.Local);
                        var CurTaskName = taskList[i].name;
                        var CurTaskID = taskList[i].id;

                        if (taskList[i].operational.lastExecutionResult.details.Count() > 0)
                        {
                            ErrorMessage = "";
                            foreach (Detail tempmsg in taskList[i].operational.lastExecutionResult.details)
                            {
                                ErrorMessage += tempmsg.message + "<br>";
                            }
                        }
                        else
                        {
                            ErrorMessage = "N/A";
                        }


                        var emailToList = "";
                        var fileData = "";
                        if (fileRefID != "00000000-0000-0000-0000-000000000000")
                        {
                            fileData = qs.GetTaskFile(CurTaskID, fileRefID);
                        }

                        //****** CHECK FOR CUSTOM PROPERTY VALUE(S) THAT CONTAIN EMAIL ADDRESSES
                        //int CustPropCount = MyTaskResult[i].customProperties.Count;

                        //yTaskResult[i].customProperties.Count

                        if (myTaskResult[i] != null)
                        {
                            if (myTaskResult[i].customProperties.Count > 0)
                            {
                                var customProp = 0;
                                for (customProp = 0;
                                    customProp <= myTaskResult[i].customProperties.Count - 1;
                                    customProp++)
                                {
                                    //custPropName = MyTaskResult[i].customProperties[customProp].definition.name;
                                    if (myTaskResult[i].customProperties[customProp].definition.name ==
                                        emailPropertyName)
                                    {
                                        if (emailToList.Length > 0)
                                        {
                                            emailToList =
                                                emailToList + "," + myTaskResult[i].customProperties[customProp].value;
                                        }
                                        else
                                        {
                                            emailToList = myTaskResult[i].customProperties[customProp].value;
                                        }
                                    }


                                }
                            }
                        }

                        //****** ASSIGN DEFAULT EMAIL ADDRESS IF NO CUSTOM VALUES ARE FOUND!
                        if (emailToList.Length == 0)
                        {
                            emailToList = emailTo;
                        }

                    //TODO: 
                    //    1. OPEN LOG FILE
                    //    2. SEARCH FOR TASKID IN LOG FILE TO DETERMINE IF AN EMAIL WAS SENT PREVIOUSLY FOR THIS TASK
                    //    3. IF AN ID IS FOUND IN FILE, 
                    //               GET DATETIME STAMP
                    //               COMPARE THIS DATE TO CURRENT DATE
                    //                    IF MORE THAN 24 HOURS 
                    //                         TRIGGER AN EMAIL PROCESS
                    //                         IF EMAIL SUCCESSFUL, WRITE THIS EVENT TO LOG FILE
                    //    4. IF NO ID THEN TRIGGER AN EMAIL
                    //    5. IF EMAIL SUCCESSFUL, WRITE THIS EVENT TO LOG FILE




                    // <---------  1. START OPEN LOG FILE & READ EACH LINE
                    // Read the file and display it line by line.
                        System.IO.StreamReader file = new System.IO.StreamReader(LogFile);
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {

                            var logParams = line.Split('~');
                            var FileTaskID = logParams[0];
                            FileLastEmailDate = Convert.ToDateTime(logParams[2]);


                            //<---------2.SEARCH FOR TASKID IN LOG FILE TO DETERMINE

                            if (FileTaskID == CurTaskID)   //IF EXISTING EMAIL IS FOUND CHECK LAST SENT DATE
                            {
                                var Emailstatus = false;
                                ExistingTask = true;
                                if (FileLastEmailDate.AddHours(24) < DateTime.Now) // Send email if last email is sent more than 24 hours ago.
                                {

                                    alertCount = +1;
                                    logger.Log(LogLevel.Information, "Sending Email For " + CurTaskName + " ....");
                                    SendEmail(CurTaskName, Convert.ToString(taskDate), emailFrom, emailUser, emailPw, emailToList, emailServer, Convert.ToInt32(emailPort), emailSsl, fileData, ErrorMessage);
                                    logger.Log(LogLevel.Information, "Email Sent!");
                                    Emailstatus = true;
                                }


                                if (Emailstatus == true)   //IF AN EMAIL HAS JUST BEEN SENT THEN MODIFY LOG FILE WITH NEW CURRENT DATE
                                {
                                    logParams[2] = Convert.ToString(DateTime.Now);

                                    line = string.Join("~", logParams);

                                }
                            }
                            Filecontent = Filecontent + line + "\r\n";
                        }


                        file.Close();

                        if (ExistingTask == false)
                        {
                            Filecontent = Filecontent + CurTaskID + "~" + CurTaskName + "~" + Convert.ToString(DateTime.Now) + "\r\n";
                            logger.Log(LogLevel.Information, "Sending Email" + CurTaskName + " ....");
                            SendEmail(CurTaskName, Convert.ToString(taskDate), emailFrom, emailUser, emailPw, emailToList, emailServer, Convert.ToInt32(emailPort), emailSsl, fileData, ErrorMessage);
                            logger.Log(LogLevel.Information, "Email Sent...");
                            alertCount = alertCount + 1;
                        }


                        if (alertCount > 0)
                        {

                            File.WriteAllText(LogFile, Filecontent);
                        }
                        file.Close();

                        if (taskDate.AddHours(24) < DateTime.Now)
                        {
                        }

                    }
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, e.ToString() +  "\r\n" + jsonData );

                    if (e.ToString() == "Timeout: The operation has timed out")
                    {
                        retval = 4;
                    }
                    else
                    {
                        retval = 8;
                    }
                }

                logger.Log( LogLevel.Information, "Returning Errorlevel " + retval.ToString() );

                logger.End();
                Environment.Exit(retval);
            }
        }


        private static void SendEmail(string TaskName, string TaskDate, string SendFrom, string UserID, string Password, string SendTo, string SMTPServer, Int32 SmtpPort, string EnableSsl, string FileData, string ErrorMsg)
        {
            var message = new MailMessage();


            byte[] byteArray = Encoding.UTF8.GetBytes(FileData);
            var stream = new MemoryStream(byteArray);

            // Create  the file attachment for this email message.
            var data = new Attachment(stream,  TaskName + "_" + TaskDate + "_Log.txt" , MediaTypeNames.Text.Plain );

            // Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(file);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            message.Attachments.Add(data);


            message.To.Add(SendTo);
            message.Subject = "Qliksense Task Failure: " + TaskName;
            
            message.From = new System.Net.Mail.MailAddress(SendFrom);
            message.IsBodyHtml = true;
            message.Body = "<h1 style=\"font-family:Arial;color: #04B431;\" >Automated QlikSense Task Failure Alert!</h1> <div style=\"font-family:Arial;\">  Following QlikSense task has failed to run:<br> <br> Task Name = <strong>"
                + TaskName + "</strong><br>Failure Date = <strong> " + TaskDate + "</strong> <br>Error = <strong> " + ErrorMsg +"</strong><br><br> No new alerts will be sent for this task for the next 24 hours! </div>";

            var smtp = new SmtpClient(SMTPServer, SmtpPort)
            {
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(UserID, Password),
                EnableSsl = EnableSsl == "Y"
            };

            smtp.Send(message);

            System.Threading.Thread.Sleep(3000);

        }

    }





}
