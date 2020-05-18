using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MyLogger;
using Newtonsoft.Json;
using QlikSenseJSONObjects;

namespace QlikSenseEmailAdmin
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var qsReg = new QlikSenseAlertRegistry();

            //usage****
            //-proxy:https://usral-msi  -timeout:10000 -task:"test123" -wait
            //***
            // Use at your own risk.  Created by Marcus Spitzmiller and Nick Akincilar.
            Console.WriteLine();
            // After looking at the code, definitely use at your own risk -- SBentley
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var logger = new Logger();
            logger.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                         "\\QlikSenseEmailAdmin\\history");
            logger.SetLogLevel(LogLevel.Debug);

            /*****************************/
            // This section is where we gather inputs from the command line and config.txt
            /*****************************/

            //defaults
            var proxy = "";

            var jsonData = "";
            var emailServer = "";
            var emailPort = "";
            var emailUser = "";
            var emailPw = "";
            var emailTo = "";
            var emailFrom = "";
            var emailSsl = "N";
            var emailPropertyName = "";

            // Delete log files older than 2 days
            DeleteOldLogFiles();

            var configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                             "\\QlikSenseEmailAdmin\\config.txt";
            var logFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\QlikSenseEmailAdmin\\log.txt";

            //global settings from config.txt
            if (File.Exists(configFile))
            {
                var congfigLines = File.ReadAllLines(configFile);
            }

            if (File.Exists(logFile))
            {
                var logLines = File.ReadAllLines(logFile);
            }

            if (args.Length == 0)
            {
                Application.Run(new frmConfig());
            }
            else
            {
                if (!args[0].Contains(":")) //Get config values from registry unless there is a parameter with ":" in it
                {
                    proxy = qsReg.GetQmcServer();

                    emailServer = qsReg.GetSmtpServer();
                    emailSsl = qsReg.GetSsl();
                    emailUser = qsReg.GetUsername();
                    emailPort = qsReg.GetPort();
                    emailPw = qsReg.GetPassword();
                    emailFrom = qsReg.GetEmailFrom();
                    emailTo = qsReg.GetEmailTo();
                    qsReg.GetWait();
                    emailPropertyName = qsReg.GetEmailPropertyName();

                    goto bypass;
                }


                var allArgs = new string[args.Length];

                args.CopyTo(allArgs, args.Length - 1);


                foreach (var arg in allArgs)
                {
                    var param = arg.Split(':');

                    switch (param[0])
                    {
                        case "-?":
                        case "/?":
                            Console.WriteLine("Usage:");
                            Console.WriteLine(
                                "-proxy:<URL address of proxy>  required example https://server.company.com");
                            Console.WriteLine("   omit -wait to return immediately");
                            Console.WriteLine("   use -wait to wait for the task to finish");
                            Console.WriteLine("     Return Codes:");
                            Console.WriteLine("     0 - task completed successfully");
                            Console.WriteLine("     4 - task timed out");
                            Console.WriteLine("     8 - task failed");
                            Console.WriteLine("");
                            Console.WriteLine("Optionally define any or all parameters in config.txt");
                            Console.WriteLine("");
                            Console.WriteLine("press any key...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        case "-proxy":
                            proxy = "";
                            for (var j = 1; j < param.Length; j++)
                            {
                                proxy += param[j];
                                if (j < param.Length - 1) proxy += ":"; //put back the colon
                            }

                            break;

                        case "-smtp_server":
                            emailServer = "";

                            for (var j = 1; j < param.Length; j++)
                            {
                                emailServer += param[j];
                                if (j < param.Length - 1) emailServer += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_port":
                            emailPort = "";

                            for (var j = 1; j < param.Length; j++)
                            {
                                emailPort += param[j];
                                if (j < param.Length - 1) emailPort += ":"; //put back the colon
                            }


                            break;

                        case "-stmp_user":
                            emailUser = "";

                            for (var j = 1; j < param.Length; j++)
                            {
                                emailUser += param[j];
                                if (j < param.Length - 1) emailUser += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_pw":

                            emailPw = "";
                            for (var j = 1; j < param.Length; j++)
                            {
                                emailPw += param[j];
                                if (j < param.Length - 1) emailPw += ":"; //put back the colon
                            }


                            break;

                        case "-smtp_from":

                            emailFrom = "";
                            for (var j = 1; j < param.Length; j++)
                            {
                                emailFrom += param[j];
                                if (j < param.Length - 1) emailFrom += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_to":

                            emailTo = "";
                            for (var j = 1; j < param.Length; j++)
                            {
                                emailTo += param[j];
                                if (j < param.Length - 1) emailTo += ":"; //put back the colon
                            }


                            break;


                        case "-smtp_enableSSL":

                            emailSsl = "";
                            for (var j = 1; j < param.Length; j++)
                            {
                                emailSsl += param[j];
                                if (j < param.Length - 1) emailSsl += ":"; //put back the colon
                            }


                            break;
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
                    var qs = new QlikSenseJSONHelper(proxy, 60000); //http timeout 60 seconds

                    logger.Log(LogLevel.Information, "Looking for Failed Tasks...");
                    //TaskStatus

                    var userList = qs.GetDeletedUserList(QsTaskStatus.Failed);

                    var taskList = qs.GetTaskByStatus(QsTaskStatus.Failed);

                    var taskListError = qs.GetTaskByStatus(QsTaskStatus.Error);

                    var taskListAborted = qs.GetTaskByStatus(QsTaskStatus.Aborted);

                    var temp = qs.GetTaskByStatusText(QsTaskStatus.Failed);
                    var myTaskResult = JsonConvert.DeserializeObject<List<TaskResult>>(temp);

                    temp = qs.GetTaskByStatusText(QsTaskStatus.Aborted);
                    jsonData = temp;
                    myTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(jsonData));

                    temp = qs.GetTaskByStatusText(QsTaskStatus.Error);
                    myTaskResult.AddRange(JsonConvert.DeserializeObject<List<TaskResult>>(temp));

                    taskList.AddRange(taskListError);
                    taskList.AddRange(taskListAborted);

                    var alertCount = 0;

                    logger.Log(LogLevel.Information, "Found " + Convert.ToString(taskList.Count) + " Failed Tasks!");

                    for (var i = 0; i <= taskList.Count - 1; i++)
                    {
                        var existingTask = false;
                        var fileContent = "";

                        var myResult = taskList[i].operational.lastExecutionResult.startTime;


                        var fileRefId = taskList[i].operational.lastExecutionResult.fileReferenceID;

                        myResult = myResult.Replace('T', ' ');
                        myResult = myResult.Replace('Z', ' ');
                        var taskDate = Convert.ToDateTime(myResult);
                        //Convert UTC time to Local Time
                        taskDate = TimeZoneInfo.ConvertTimeFromUtc(taskDate, TimeZoneInfo.Local);
                        var curTaskName = taskList[i].name;
                        var curTaskId = taskList[i].id;

                        var errorMessage = "";
                        errorMessage = taskList[i].operational.lastExecutionResult.details.Any()
                            ? taskList[i].operational.lastExecutionResult.details.Aggregate("",
                                (current, tempmsg) => current + tempmsg.message + "<br>")
                            : "N/A";


                        var emailToList = "";
                        var fileData = "";
                        if (fileRefId != "00000000-0000-0000-0000-000000000000")
                            fileData = qs.GetTaskFile(curTaskId, fileRefId);

                        //****** CHECK FOR CUSTOM PROPERTY VALUE(S) THAT CONTAIN EMAIL ADDRESSES

                        if (myTaskResult[i] != null && myTaskResult[i].customProperties.Count > 0)
                        {
                            var customProp;
                            for (customProp = 0;
                                customProp <= myTaskResult[i].customProperties.Count - 1;
                                customProp++)
                                if (myTaskResult[i].customProperties[customProp].definition.name ==
                                    emailPropertyName)
                                {
                                    if (emailToList.Length > 0)
                                        emailToList += "," + myTaskResult[i].customProperties[customProp].value;
                                    else
                                        emailToList = myTaskResult[i].customProperties[customProp].value;
                                }
                        }

                        //****** ASSIGN DEFAULT EMAIL ADDRESS IF NO CUSTOM VALUES ARE FOUND!
                        if (emailToList.Length == 0) emailToList = emailTo;

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
                        var file = new StreamReader(logFile);
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            var logParams = line.Split('~');
                            var fileTaskId = logParams[0];
                            var fileLastEmailDate = Convert.ToDateTime(logParams[2]);


                            //<---------2.SEARCH FOR TASKID IN LOG FILE TO DETERMINE

                            if (fileTaskId == curTaskId) //IF EXISTING EMAIL IS FOUND CHECK LAST SENT DATE
                            {
                                var emailStatus = false;
                                existingTask = true;
                                if (fileLastEmailDate.AddHours(24) < DateTime.Now) // Send email if last email is sent more than 24 hours ago.
                                {
                                    alertCount = +1;
                                    logger.Log(LogLevel.Information, "Sending Email For " + curTaskName + " ....");
                                    SendEmail(curTaskName, Convert.ToString(taskDate), emailFrom, emailUser, emailPw,
                                        emailToList, emailServer, Convert.ToInt32(emailPort), emailSsl, fileData,
                                        errorMessage);
                                    logger.Log(LogLevel.Information, "Email Sent!");
                                    emailStatus = true;
                                }


                                if (emailStatus
                                ) //IF AN EMAIL HAS JUST BEEN SENT THEN MODIFY LOG FILE WITH NEW CURRENT DATE
                                {
                                    logParams[2] = Convert.ToString(DateTime.Now);
                                    line = string.Join("~", logParams);
                                }
                            }

                            fileContent += line + "\r\n";
                        }

                        file.Close();

                        if (!existingTask)
                        {
                            fileContent = fileContent + curTaskId + "~" + curTaskName + "~" +
                                          Convert.ToString(DateTime.Now) + "\r\n";
                            logger.Log(LogLevel.Information, "Sending Email" + curTaskName + " ....");
                            SendEmail(curTaskName, Convert.ToString(taskDate), emailFrom, emailUser, emailPw,
                                emailToList, emailServer, Convert.ToInt32(emailPort), emailSsl, fileData, errorMessage);
                            logger.Log(LogLevel.Information, "Email Sent...");
                            alertCount++;
                        }


                        if (alertCount > 0) File.WriteAllText(logFile, fileContent);
                        file.Close();
                    }
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, e + "\r\n" + jsonData);

                    if (e.ToString() == "Timeout: The operation has timed out")
                        retval = 4;
                    else
                        retval = 8;
                }

                logger.Log(LogLevel.Information, "Returning Errorlevel " + retval);

                logger.End();
                Environment.Exit(retval);
            }
        }

        private static void DeleteOldLogFiles()
        {
            var files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                          "\\QlikSenseEmailAdmin\\history").GetFiles("*.log");
            foreach (var file in files)
                if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromDays(2))
                    File.Delete(file.FullName);
        }

        private static void SendEmail(string taskName, string taskDate, string sendFrom, string userId, string password,
            string sendTo, string smtpServer, int smtpPort, string enableSsl, string fileData, string errorMsg)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (smtpServer == null) throw new ArgumentNullException(nameof(smtpServer));
            if (fileData == null) throw new ArgumentNullException(nameof(fileData));
            var message = new MailMessage();


            var byteArray = Encoding.UTF8.GetBytes(fileData);
            var stream = new MemoryStream(byteArray);

            // Create  the file attachment for this email message.
            var data = new Attachment(stream, taskName + "_" + taskDate + "_Log.txt", MediaTypeNames.Text.Plain);

            // Add time stamp information for the file.
            // Add the file attachment to this email message.
            message.Attachments.Add(data);
            message.To.Add(sendTo);
            message.Subject = "Qliksense Task Failure: " + taskName;

            message.From = new MailAddress(sendFrom);
            message.IsBodyHtml = true;
            message.Body =
                "<h1 style=\"font-family:Arial;color: #04B431;\" >Automated QlikSense Task Failure Alert!</h1> <div style=\"font-family:Arial;\">  Following QlikSense task has failed to run:<br> <br> Task Name = <strong>"
                + taskName + "</strong><br>Failure Date = <strong> " + taskDate + "</strong> <br>Error = <strong> " +
                errorMsg + "</strong><br><br> No new alerts will be sent for this task for the next 24 hours! </div>";

            var smtp = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(userId, password),
                EnableSsl = enableSsl == "Y"
            };

            smtp.Send(message);

            Thread.Sleep(3000);
        }
    }
}