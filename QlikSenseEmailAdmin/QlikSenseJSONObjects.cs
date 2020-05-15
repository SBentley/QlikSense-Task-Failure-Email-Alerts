using System;
using System.Collections.Generic;
namespace QlikSenseJSONObjects
{
    // - /qrs/about
    public class QlikSenseAbout
    {
        public string buildVersion { get; set; }
        public string buildDate { get; set; }
        public string databaseProvider { get; set; }
        public int nodeType { get; set; }
        public string schemaPath { get; set; }
    }

    public class QlikSenseStream
    {
        public string name { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
    }

    public class QlikSenseApp
    {
        public string name { get; set; }
        public string appId { get; set; }
        public string publishTime { get; set; }
        public bool published { get; set; }
        public QlikSenseStream stream { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
    }


    public class QlikSenseCreateReloadTaskRequest
    {
        public string task { get; set; }
    }

    public class QInfo
    {
        public string qId { get; set; }
        public string qType { get; set; }
    }

    public class QSortCriteria
    {
        public int qSortByLoadOrder { get; set; }
    }

    public class QDef
    {
        public List<string> qFieldDefs { get; set; }
        public List<string> qFieldLabels { get; set; }
        public List<QSortCriteria> qSortCriterias { get; set; }
    }

    public class QInitialDataFetch
    {
        public int qTop { get; set; }
        public int qHeight { get; set; }
        public int qLeft { get; set; }
        public int qWidth { get; set; }
    }

    public class QListObjectDef
    {
        public string qStateName { get; set; }
        public string qLibraryId { get; set; }
        public QDef qDef { get; set; }
        public List<QInitialDataFetch> qInitialDataFetch { get; set; }
    }

    public class CreateSessionObjectParam
    {
        public QInfo qInfo { get; set; }
        public QListObjectDef qListObjectDef { get; set; }
    }

    public class CreateSessionObject
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public string method { get; set; }
        public int handle { get; set; }
        public List<CreateSessionObjectParam> @params { get; set; }
    }


    public class QlikSenseTaskApp
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class QlikSenseTask
    {
        public string name { get; set; }
        public int taskType { get; set; }
        public bool enabled { get; set; }
        public int taskSessionTimeout { get; set; }
        public int maxRetries { get; set; }
        public List<object> tags { get; set; }
        public QlikSenseTaskApp app { get; set; }
        public bool isManuallyTriggered { get; set; }
        public List<object> customProperties { get; set; }
    }

    public class QlikSenseCreateTask
    {
        public QlikSenseTask task { get; set; }
    }

    public class QlikSenseTaskAppResult
    {
        public string name { get; set; }
        public string appId { get; set; }
        public string publishTime { get; set; }
        public bool published { get; set; }
        public object stream { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
    }

    public class QlikSenseTaskExecutionGuid
    {
        public string value { get; set; }
    }




    public class QlikSenseTaskLastExecutionResult
    {
        public int status { get; set; }
        public string startTime { get; set; }
        public string stopTime { get; set; }
        public int duration { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
        public string fileReferenceID { get; set; }
    }

    public class QlikSenseTaskOperational
    {
        public QlikSenseTaskLastExecutionResult lastExecutionResult { get; set; }
        public string nextExecution { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
        public string fileReferenceID { get; set; }
    }

    public class QlikSenseCreateTaskResult
    {
        public List<object> customProperties { get; set; }
        public QlikSenseTaskAppResult app { get; set; }
        public bool isManuallyTriggered { get; set; }
        public QlikSenseTaskOperational operational { get; set; }
        public string name { get; set; }
        public int taskType { get; set; }
        public bool enabled { get; set; }
        public int taskSessionTimeout { get; set; }
        public int maxRetries { get; set; }
        public List<object> tags { get; set; }
        public List<string> privileges { get; set; }
        public string id { get; set; }
        public string createdDate { get; set; }
        public string modifiedDate { get; set; }
        public string modifiedByUserName { get; set; }
        public string schemaPath { get; set; }
    }

 

    public class QlikSenseTaskResultLastExecutionResult
    {

        //public QlikSenseTaskResultLastExecutionResultDetails details { get; set; }

        public IList<Detail> details { get; set; }

        public int status { get; set; }
        public string startTime { get; set; }
        public string stopTime { get; set; }
        public int duration { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }
        public string fileReferenceID { get; set; }

    }

    public class QlikSenseTaskResultLastExecutionResultDetails
    {
        public string id { get; set; }
        public string message { get; set; }
    }

    public class QlikSenseTaskResultOperational
    {
        public QlikSenseTaskResultLastExecutionResult lastExecutionResult { get; set; }
        
        public string nextExecution { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }

   
    }

    public class QlikSenseTaskResult
    {
        public QlikSenseTaskResultOperational operational { get; set; }
        public QlikSenseTaskResultCustomProperties CustProp { get; set; }
        public string name { get; set; }
        public int taskType { get; set; }
        public bool enabled { get; set; }
        public object privileges { get; set; }
        public string id { get; set; }

    }



    public class QlikSenseUserList
    {

        public string name { get; set; }
        public string inactive { get; set; }
        public bool RemovedExternally { get; set; }
        public string userid { get; set; }
        public string id { get; set; }
    }


    public class QlikSenseTaskResultCustomProperties
    {
        //public QlikSenseTaskResultLastExecutionResult lastExecutionResult { get; set; }
        public string value { get; }

        public string id { get; }
    }



    //***************

    public class Definition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string valueType { get; set; }
        public IList<string> choiceValues { get; set; }
        public object privileges { get; set; }
    }

    public class CustomProperty
    {
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedByUserName { get; set; }
        public string value { get; set; }
        public Definition definition { get; set; }
        public string schemaPath { get; set; }
    }

    public class App
    {
        public string id { get; set; }
        public string name { get; set; }
        public string appId { get; set; }
        public DateTime publishTime { get; set; }
        public bool published { get; set; }
        public object stream { get; set; }
        public string savedInProductVersion { get; set; }
        public string migrationHash { get; set; }
        public int availabilityStatus { get; set; }
        public object privileges { get; set; }
    }

    public class Detail
    {
        public string id { get; set; }
        public int detailsType { get; set; }
        public string message { get; set; }
        public DateTime detailCreatedDate { get; set; }
        public object privileges { get; set; }
    }

    public class LastExecutionResult
    {
        public string id { get; set; }
        public string executingNodeName { get; set; }
        public int status { get; set; }
        public DateTime startTime { get; set; }
        public DateTime stopTime { get; set; }
        public int duration { get; set; }
        public string fileReferenceID { get; set; }
        public bool scriptLogAvailable { get; set; }
        public IList<Detail> details { get; set; }
        public object privileges { get; set; }
        
    }

    public class Operational
    {
        public string id { get; set; }
        public LastExecutionResult lastExecutionResult { get; set; }
        public DateTime nextExecution { get; set; }
        public object privileges { get; set; }
    }

    public class TaskResult
    {
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public string modifiedByUserName { get; set; }
        public IList<CustomProperty> customProperties { get; set; }
        public App app { get; set; }
        public bool isManuallyTriggered { get; set; }
        public Operational operational { get; set; }
        public string name { get; set; }
        public int taskType { get; set; }
        public bool enabled { get; set; }
        public int taskSessionTimeout { get; set; }
        public int maxRetries { get; set; }
        public IList<object> tags { get; set; }
        public object privileges { get; set; }
        public string schemaPath { get; set; }
    }





}