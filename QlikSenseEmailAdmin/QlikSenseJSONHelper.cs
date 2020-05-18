using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QlikSenseJSONObjects;
using QVnextDemoBuilder;

namespace QlikSenseEmailAdmin
{
    enum QsTaskStatus { NeverStarted, Triggered, Started, Queued, AbortInitiated, Aborting, Aborted, Success, Failed, Skipped, Retrying, Error, Reset }

    class QlikSenseJSONHelper
    {
        private readonly QRSNTLMWebClient _qrsClient;

        public QlikSenseJSONHelper(string url, int timeout)
        {
            _qrsClient = new QRSNTLMWebClient(url, timeout);
        }

        public string GetAppID(string appname)
        {
            Dictionary<string, string> appqueries = new Dictionary<string, string>();
            appqueries.Add("filter", "name eq '" + appname + "'");

            //find the app
            var appid = "";
            var appString = _qrsClient.Get("/qrs/app", appqueries);
            var apps = JsonConvert.DeserializeObject<List<QlikSenseApp>>(appString);
            foreach (var app in apps.Where(app => app.name == appname))
            {
                appid = app.id;
            }

            if (appid == "")
            {
                throw new Exception("Couldn't find app");
            }
            return appid;
        }

        public int GetTaskStatusByName(string taskname)
        {
            var queries = new Dictionary<string, string>();
            queries.Add("filter", "name eq '" + taskname + "'");

            //find the app
            var taskString = _qrsClient.Get("/qrs/task", queries);
            var tasks = JsonConvert.DeserializeObject<List<QlikSenseTaskResult>>(taskString);

            var retval = -1;
            if (tasks.Count == 1)
            {
                retval = tasks[0].operational.lastExecutionResult.status;
            }
            return retval;
        }



        public List<QlikSenseTaskResult> GetTaskByStatus(QsTaskStatus status)
        {
            var queries = new Dictionary<string, string>
            {
                {
                    "filter", "operational.lastExecutionResult.status eq " + Convert.ToString((int) status) +
                              " and enabled eq true"
                }
            };

            //find the app
            var taskString = _qrsClient.Get("/qrs/task/full", queries);
            var tasks = JsonConvert.DeserializeObject<List<QlikSenseTaskResult>>(taskString);
            
            return tasks;
        }

        public string GetTaskByStatusText(QsTaskStatus status)
        {
            var queries = new Dictionary<string, string>
            {
                {
                    "filter", "operational.lastExecutionResult.status eq " + Convert.ToString((int) status) +
                              " and enabled eq true"
                }
            };

            var taskString = _qrsClient.Get("/qrs/task/full", queries);

            return taskString;
        }


        public List<QlikSenseTaskResultLastExecutionResult> GetTaskDetailsByStatus(TaskStatus status)
        {
            Dictionary<string, string> queries = new Dictionary<string, string>();



            queries.Add("filter", "operational.lastExecutionResult.status eq " + Convert.ToString((int)status));

            //find the app
            var taskString = _qrsClient.Get("/qrs/task/full", queries);
            var tasks = JsonConvert.DeserializeObject<List<QlikSenseTaskResultLastExecutionResult>>(taskString);

            var retval = -1;

            return tasks;
        }

        public int GetTaskStatus(QlikSenseTaskExecutionGuid taskexecutionguid)
        {
            var queries = new Dictionary<string, string>();

            //find the app
            var taskString = _qrsClient.Get("/qrs/executionresult/"+taskexecutionguid.value, queries);
            var tasks = JsonConvert.DeserializeObject<List<QlikSenseTaskResult>>(taskString);

            var retval = 0;
            if (tasks.Count == 1)
            {
                retval = tasks[0].operational.lastExecutionResult.status;
            }

            return retval;
        }

        public string GetTaskFile(string Taskid, string FileRefID)
        {
            var queries = new Dictionary<string, string>();
            var taskstring = _qrsClient.Get("/qrs/ReloadTask/" + Taskid + "/scriptlog?fileReferenceId=" + FileRefID + "&xrfkey=ABCDEFG123456789", queries );
            
            taskstring = taskstring.Substring(10,36);

            taskstring = _qrsClient.Get("/qrs/download/reloadtask/" + taskstring + "/Mylog.txt&xrfkey=ABCDEFG123456789", queries);

            return taskstring;
        }


        public string GetStreamID(string streamname)
        {
            var streamQueries = new Dictionary<string, string>();
            streamQueries.Add("filter", "name eq '" + streamname + "'");

            //find the stream
            var streamId = "";
            var streamString = _qrsClient.Get("/qrs/stream", streamQueries);
            var streams = JsonConvert.DeserializeObject<List<QlikSenseStream>>(streamString);
            foreach (var stream in streams.Where(stream => stream.name == streamname))
            {
                streamId = stream.id;
            }

            if (streamId == "")
            {
                throw new Exception("Couldn't find stream");
            }
            return streamId;
        }

        public QlikSenseApp GetApp(string appName)
        {
            var appQueries = new Dictionary<string, string>();
            appQueries.Add("filter", "name eq '" + appName + "'");

            //find the app
            var appString = _qrsClient.Get("/qrs/app", appQueries);
            var apps = JsonConvert.DeserializeObject<List<QlikSenseApp>>(appString);
            return apps.FirstOrDefault(app => app.name == appName);
        }

        public string CopyApp(string appid, string name)
        {
            var endpoint = "/qrs/app/" + appid + "/copy";
            var newAppJson = "";
            if (name != "")
            {
                var queries = new Dictionary<string, string> {{"name", name}};
                newAppJson = _qrsClient.Post(endpoint, queries);
            }
            else
            {
                newAppJson = _qrsClient.Post(endpoint, "");
            }
            var newApp = JsonConvert.DeserializeObject<QlikSenseApp>(newAppJson);
            return newApp.id;
        }

        public void Reload(string appid)
        {
            _qrsClient.Post("/qrs/app/" + appid + "/reload", "");
        }

        public void StartTask(string taskid, bool synchronous)
        {
            var path = "/qrs/task/" + taskid + "/start";
            if (synchronous) path += "/synchronous";

            _qrsClient.Post(path, "");
        }

        public QlikSenseTaskExecutionGuid StartTaskByName(string taskName, bool synchronous)
        {
            var path = "/qrs/task/start";
            if (synchronous) path += "/synchronous";

            var queries = new Dictionary<string, string> {{"name", taskName}};

            string newTaskJsonOut;
            try
            {
                newTaskJsonOut = _qrsClient.Post(path, queries);
            }
            catch (Exception e)
            {
                throw new HttpRequestException("Error executing post request", e);
            }

            return JsonConvert.DeserializeObject<QlikSenseTaskExecutionGuid>(newTaskJsonOut);
            //should change to use the execution result API
        }

        public void Publish(string appid, string streamId)
        {
            try
            {
                var appQueries = new Dictionary<string, string> {{"stream", streamId}};

                _qrsClient.Put("/qrs/app/" + appid + "/publish", appQueries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Replace(string newappid, string oldappid)
        {
            try
            {
                _qrsClient.Post("/qrs/app/" + newappid + "/replace", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Delete(string appid)
        {
            _qrsClient.Delete("/qrs/app/" + appid);
        }

        public string CreateTask(string appid, string appname, string taskname)
        {
            const string endpoint = "/qrs/reloadtask/create";

            var task = new QlikSenseCreateTask
            {
                task = new QlikSenseTask
                {
                    name = taskname,
                    taskType = 0,
                    enabled = true,
                    taskSessionTimeout = 1440,
                    maxRetries = 0,
                    tags = new List<object>(),
                    app = new QlikSenseTaskApp {id = appid, name = appname}
                }
            };

            task.task.isManuallyTriggered = true;
            task.task.customProperties = new List<object>();

            var newTaskJsonIn = JsonConvert.SerializeObject(task);

            var newTaskJsonOut = _qrsClient.Post(endpoint, newTaskJsonIn);
            var result = JsonConvert.DeserializeObject<QlikSenseCreateTaskResult>(newTaskJsonOut);
            
            return result.id;
        }

        public List<QlikSenseUserList> GetDeletedUserList(QsTaskStatus status)
        {
            var queries = new Dictionary<string, string> {{"filter", "removedExternally+eq+True"}};
            var userString = _qrsClient.Get("/qrs/User/full/", queries);
            var users = JsonConvert.DeserializeObject<List<QlikSenseUserList>>(userString);

            return users;
        }
    }
}
