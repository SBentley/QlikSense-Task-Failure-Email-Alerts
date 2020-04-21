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
            // Int32 timeout = 60 * 1000;
            string task = "";
            // bool synchronous = true;

            string JsonData = "";
            string email_server = "";
            string email_port = "";
            string email_user = "";
            string email_pw = "";
            string email_to = "";
            string email_from = "";
            string email_ssl = "N";
            string email_property_name = "";
            string qs_wait = "60000";





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

                    email_server = QSReg.GetSMTPserver();
                    email_ssl = QSReg.GetSSL();
                    email_user = QSReg.GetUsername();
                    email_port = QSReg.GetPort();
                    email_pw = QSReg.GetPassword();
                    email_from = QSReg.GetEmailFrom();
                    email_to = QSReg.GetEmailTo();
                    qs_wait = QSReg.GetWait();
                    email_property_name = QSReg.GetEmailPropertyName();

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
                            email_server = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                email_server += param[j];
                                if (j < param.Length - 1) email_server += ":"; //put back the colon
                            }


                            break;



                        case "-smtp_port":
                            email_port = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                email_port += param[j];
                                if (j < param.Length - 1) email_port += ":"; //put back the colon
                            }


                            break;

                        case "-stmp_user":
                            email_user = "";

                            for (int j = 1; j < param.Length; j++)
                            {
                                email_user += param[j];
                                if (j < param.Length - 1) email_user += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_pw":

                            email_pw = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                email_pw += param[j];
                                if (j < param.Length - 1) email_pw += ":"; //put back the colon
                            }


                            break;

                        case "-smtp_from":

                            email_from = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                email_from += param[j];
                                if (j < param.Length - 1) email_from += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_to":

                            email_to = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                email_to += param[j];
                                if (j < param.Length - 1) email_to += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_enableSSL":

                            email_ssl = "";
                            for (int j = 1; j < param.Length; j++)
                            {
                                email_ssl += param[j];
                                if (j < param.Length - 1) email_ssl += ":"; //put back the colon
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

            bypass:;
                logger.Log(LogLevel.Information, "Proxy: " + proxy);
   
                if (proxy == "")
                {
                    logger.Log(LogLevel.Fatal, "Proxy or undefined");

                    Environment.Exit(4);
                }




                int retval = 0;
                try
                {
                    QlikSenseJSONHelper qs = new QlikSenseJSONHelper(proxy, 60000, logger); //http timeout 60 seconds
                                                                                            //SMTPMail mail = new SMTPMail();
                                                                                            //TODO: - set mail params SMTPServerHost, and SMTPServerPort, AdminEmailAddress, FromEmailAddress,EnvironmentName there will probably be a couple more

                    logger.Log(LogLevel.Information, "Looking for Failed Tasks...");
                    //TaskStatus

                    List<QlikSenseUserList> UserList = (List<QlikSenseUserList>)qs.GetDeletedUserList(QSTaskStatus.Failed);

                    List<QlikSenseTaskResult> TaskList = (List<QlikSenseTaskResult>)qs.GetTaskByStatus(QSTaskStatus.Failed);

                    List<QlikSenseTaskResult> TaskListError = (List<QlikSenseTaskResult>)qs.GetTaskByStatus(QSTaskStatus.Error);

                    List<QlikSenseTaskResult> TaskListAborted = (List<QlikSenseTaskResult>)qs.GetTaskByStatus(QSTaskStatus.Aborted);


                    //TaskList.AddRange(TaskListError);
                    //TaskList.AddRange(TaskListAborted);

                    //***

                    string temp;

                    //Nick----8/9/2017
                    temp = qs.GetTaskByStatusText(QSTaskStatus.Failed);
                   
                 //   JsonData = temp;
                    List<TaskResult> MyTaskResult = JsonConvert.DeserializeObject<List<TaskResult>>(temp);


                    //temp =  qs.GetTaskByStatusText(QSTaskStatus.Aborted);
                    //List<TaskResult> MyTaskResultAbort = JsonConvert.DeserializeObject<List<TaskResult>>(temp);

                    //MyTaskResult = MyTaskResult.AddRange(MyTaskResultAbort);


                    temp = qs.GetTaskByStatusText(QSTaskStatus.Aborted);
                    JsonData = temp;
                    MyTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(JsonData));
                    //List<TaskResult> MyTaskResult = JsonConvert.DeserializeObject<List<TaskResult>>(temp);

                    temp = qs.GetTaskByStatusText(QSTaskStatus.Error);
                    MyTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(temp));


                    //****


                    TaskList.AddRange(TaskListError);
                    TaskList.AddRange(TaskListAborted);

                    //List<Customproperty> customPropertyList;

                    string myresult;
                    string errormsg;
                    string CurTaskID;
                    string CurTaskName;
                    string CurTaskDate;
                    object customProperty;
                    DateTime TaskDate;

                    int counter = 0;
                    int customProp = 0;
                    string line;
                    string Filecontent = "";
                    string[] logParams;
                    string FileTaskID = "";
                    string FileTaskName = "";
                    Boolean Emailstatus = false;
                    Boolean ExistingTask = false;
                    string emailToList = "";
                    string custPropName = "";
                    int AlertCount = 0;
                    string ErrorMessage = "";

                    string fileRefID = "";
                    string fileData = "";

                    DateTime FileLastEmailDate;

                    logger.Log(LogLevel.Information, "Found " + Convert.ToString(TaskList.Count) + " Failed Tasks!");



                    for (int i = 0; i <= TaskList.Count - 1; i++)
                    {
                        ExistingTask = false;
                        Filecontent = "";

                        myresult = TaskList[i].operational.lastExecutionResult.startTime;


                        fileRefID = TaskList[i].operational.lastExecutionResult.fileReferenceID;



                        // List<QlikSenseTaskResult> TaskList = (List<QlikSenseTaskResult>)qs.GetTaskByStatus(QSTaskStatus.Failed);



                        myresult = myresult.Replace('T', ' ');
                        myresult = myresult.Replace('Z', ' ');
                        TaskDate = Convert.ToDateTime(myresult);
                        //Convert UTC time to Local Time
                        TaskDate = TimeZoneInfo.ConvertTimeFromUtc(TaskDate, TimeZoneInfo.Local);
                        CurTaskName = TaskList[i].name;
                        CurTaskID = TaskList[i].id;

                        if (TaskList[i].operational.lastExecutionResult.details.Count() > 0)
                        {
                            ErrorMessage = "";
                            foreach (QlikSenseJSONObjects.Detail tempmsg in TaskList[i].operational.lastExecutionResult.details)
                            {
                                ErrorMessage += tempmsg.message + "<br>";
                            }
                        }
                        else
                        {
                            ErrorMessage = "N/A";
                        }


                        emailToList = "";
                        fileData = "";
                        if (fileRefID != "00000000-0000-0000-0000-000000000000")
                        {
                            fileData = qs.GetTaskFile(CurTaskID, fileRefID);
                        }



                        //****** CHECK FOR CUSTOM PROPERTY VALUE(S) THAT CONTAIN EMAIL ADDRESSES
                        //int CustPropCount = MyTaskResult[i].customProperties.Count;


                        //yTaskResult[i].customProperties.Count

                        if (MyTaskResult[i] != null)
                        { 
                            if (MyTaskResult[i].customProperties.Count > 0)
                            {

                                for (customProp = 0; customProp <= MyTaskResult[i].customProperties.Count - 1; customProp++)
                                {
                                    //custPropName = MyTaskResult[i].customProperties[customProp].definition.name;
                                    if (MyTaskResult[i].customProperties[customProp].definition.name == email_property_name)
                                    {
                                        if (emailToList.Length > 0)
                                        {
                                            emailToList = emailToList + "," + MyTaskResult[i].customProperties[customProp].value;
                                        }
                                        else
                                        {
                                            emailToList = MyTaskResult[i].customProperties[customProp].value;
                                        }
                                    }


                                }
                            }
                    }

                        //****** ASSIGN DEFAULT EMAIL ADDRESS IF NO CUSTOM VALUES ARE FOUND!
                        if (emailToList.Length == 0)
                        {
                            emailToList = email_to;
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



                    FileTaskID = "";
                        FileTaskName = "";
                        Emailstatus = false;


                        // Read the file and display it line by line.
                        System.IO.StreamReader file = new System.IO.StreamReader(LogFile);
                        while ((line = file.ReadLine()) != null)
                        {

                            logParams = line.Split('~');
                            FileTaskID = logParams[0];
                            FileTaskName = logParams[1];
                            FileLastEmailDate = Convert.ToDateTime(logParams[2]);


                            //<---------2.SEARCH FOR TASKID IN LOG FILE TO DETERMINE

                            if (FileTaskID == CurTaskID)   //IF EXISTING EMAIL IS FOUND CHECK LAST SENT DATE
                            {
                                Emailstatus = false;
                                ExistingTask = true;
                                if (FileLastEmailDate.AddHours(24) < DateTime.Now) // Send email if last email is sent more than 24 hours ago.
                                {

                                    AlertCount = +1;
                                    logger.Log(LogLevel.Information, "Sending Email For " + CurTaskName + " ....");
                                    //SendEmail(CurTaskName, Convert.ToString(TaskDate), email_from, email_user, email_pw, email_to, email_server, Convert.ToInt32(email_port), email_ssl);
                                    SendEmail(CurTaskName, Convert.ToString(TaskDate), email_from, email_user, email_pw, emailToList, email_server, Convert.ToInt32(email_port), email_ssl, fileData, ErrorMessage);
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
                            counter++;
                        }


                        file.Close();

                        if (ExistingTask == false)
                        {
                            Filecontent = Filecontent + CurTaskID + "~" + CurTaskName + "~" + Convert.ToString(DateTime.Now) + "\r\n";
                            logger.Log(LogLevel.Information, "Sending Email" + CurTaskName + " ....");
                            SendEmail(CurTaskName, Convert.ToString(TaskDate), email_from, email_user, email_pw, emailToList, email_server, Convert.ToInt32(email_port), email_ssl, fileData, ErrorMessage);
                            logger.Log(LogLevel.Information, "Email Sent...");
                            AlertCount = AlertCount + 1;
                        }


                        if (AlertCount > 0)
                        {

                            File.WriteAllText(LogFile, Filecontent);
                        }
                        file.Close();




                        // <---------  1. FINISH OPEN LOG FILE & READ EACH LINE


                        if (TaskDate.AddHours(24) < DateTime.Now)
                        {
                            errormsg = "Task Failed:" + TaskList[i].name + " on " + Convert.ToString(TaskDate);
                        }

                        //Console.WriteLine(i);
                    }







 
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, e.ToString() +  "\r\n" + JsonData );

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


        private static Boolean SendEmail(string TaskName, string TaskDate, string SendFrom, string UserID, string Password, string SendTo, string SMTPServer, Int32 SmtpPort, string EnableSsl, string FileData, string ErrorMsg)
        {
            MailMessage message = new MailMessage();


            byte[] byteArray = Encoding.UTF8.GetBytes(FileData);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);

            // Create  the file attachment for this email message.
            Attachment data = new Attachment(stream,  TaskName + "_" + TaskDate + "_Log.txt" , MediaTypeNames.Text.Plain );

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
            
            SmtpClient smtp = new SmtpClient(SMTPServer, SmtpPort);
            smtp.UseDefaultCredentials = false;
          
            smtp.Credentials = new System.Net.NetworkCredential(UserID, Password);
            if (EnableSsl == "Y")
            {
                smtp.EnableSsl = true;
            }
            else
            {
                smtp.EnableSsl = false;
            }
            
            smtp.Send(message);

            System.Threading.Thread.Sleep(3000);
            return true;

        }

    }





}
