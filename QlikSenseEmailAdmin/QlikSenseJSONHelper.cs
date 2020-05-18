using System;
using System.Collections.Generic;
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

        public string GetTaskFile(string Taskid, string FileRefID)
        {
            var queries = new Dictionary<string, string>();
            var taskstring = _qrsClient.Get("/qrs/ReloadTask/" + Taskid + "/scriptlog?fileReferenceId=" + FileRefID + "&xrfkey=ABCDEFG123456789", queries );
            
            taskstring = taskstring.Substring(10,36);

            taskstring = _qrsClient.Get("/qrs/download/reloadtask/" + taskstring + "/Mylog.txt&xrfkey=ABCDEFG123456789", queries);

            return taskstring;
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
