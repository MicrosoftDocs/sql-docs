---
title: "Custom Messages for Logging | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [Integration Services], custom"
  - "writing log entries"
  - "SSIS packages, logs"
  - "custom messages for logging [Integration Services]"
ms.assetid: 3c74bba9-02b7-4bf5-bad5-19278b680730
author: janinezhang
ms.author: janinez
manager: craigg
---
# Custom Messages for Logging
  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides a rich set of custom events for writing log entries for packages and many tasks. You can use these entries to save detailed information about execution progress, results, and problems by recording predefined events or user-defined messages for later analysis. For example, you can record when a bulk insert begins and ends to identify performance issues when the package runs.  
  
 The custom log entries are a different set of entries than the set of standard logging events that are available for packages and all containers and tasks. The custom log entries are tailored to capture useful information about a specific task in a package. For example, one of the custom log entries for the Execute SQL task records the SQL statement that the task executes in the log.  
  
 All log entries include date and time information, including the log entries that are automatically written when a package begins and finishes. Many of the log events write multiple entries to the log. This typically occurs when the event has different phases. For example, the `ExecuteSQLExecutingQuery` log event writes three entries: one entry after the task acquires a connection to the database, another after the task starts to prepare the SQL statement, and one more after the execution of the SQL statement is completed.  
  
 The following [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] objects have custom log entries:  
  
 [Package](#Package)  
  
 [Bulk Insert Task](#BulkInsert)  
  
 [Data Flow Task](#DataFlow)  
  
 [Execute DTS 2000 Task](#ExecuteDTS200)  
  
 [Execute Process Task](#ExecuteProcess)  
  
 [Execute SQL Task](#ExecuteSQL)  
  
 [File System Task](#FileSystem)  
  
 [FTP Task](#FTP)  
  
 [Message Queue Task](#MessageQueue)  
  
 [Script Task](#Script)  
  
 [Send Mail Task](#SendMail)  
  
 [Transfer Database Task](#TransferDatabase)  
  
 [Transfer Error Messages Task](#TransferErrorMessages)  
  
 [Transfer Jobs Task](#TransferJobs)  
  
 [Transfer Logins Task](#TransferLogins)  
  
 [Transfer Master Stored Procedures Task](#TransferMasterStoredProcedures)  
  
 [Transfer SQL Server Objects Task](#TransferSQLServerObjects)  
  
 [Web Services Task](#WebServices)  
  
 [WMI Data Reader Task](#WMIDataReader)  
  
 [WMI Event Watcher Task](#WMIEventWatcher)  
  
 [XML Task](#XML)  
  
## Log Entries  
  
###  <a name="Package"></a> Package  
 The following table lists the custom log entries for packages.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`PackageStart`|Indicates that the package began to run.<br /><br /> Note: This log entry is automatically written to the log. You cannot exclude it.|  
|`PackageEnd`|Indicates that the package completed.<br /><br /> Note: This log entry is automatically written to the log. You cannot exclude it.|  
|`Diagnostic`|Provides information about the system configuration that affects package execution such as the number executables that can be run concurrently.<br /><br /> The `Diagnostic` log entry also includes before and after entries for calls to external data providers. For more information, see [Troubleshooting Tools Package Connectivity](troubleshooting/troubleshooting-tools-for-package-connectivity.md).|  
  
###  <a name="BulkInsert"></a> Bulk Insert Task  
 The following table lists the custom log entries for the Bulk Insert task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`DTSBulkInsertTaskBegin`|Indicates that the bulk insert began.|  
|`DTSBulkInsertTaskEnd`|Indicates that the bulk insert finished.|  
|`DTSBulkInsertTaskInfos`|Provides descriptive information about the task.|  
  
###  <a name="DataFlow"></a> Data Flow Task  
 The following table lists the custom log entries for the Data Flow task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`BufferSizeTuning`|Indicates that the Data Flow task changed the size of the buffer. The log entry describes the reasons for the size change and lists the temporary new buffer size.|  
|`OnPipelinePostEndOfRowset`|Denotes that a component has been given its end-of-rowset signal, which is set by the last call of the `ProcessInput` method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|`OnPipelinePostPrimeOutput`|Indicates that the component has completed its last call to the `PrimeOutput` method. Depending on the data flow, multiple log entries may be written. If the component is a source, this means that the component has finished processing rows.|  
|`OnPipelinePreEndOfRowset`|Indicates that a component is about to receive its end-of-rowset signal, which is set by the last call of the `ProcessInput` method. An entry is written for each component in the data flow that processes input. The entry includes the name of the component.|  
|`OnPipelinePrePrimeOutput`|Indicates that the component is about to receive its call from the `PrimeOutput` method. Depending on the data flow, multiple log entries may be written.|  
|`OnPipelineRowsSent`|Reports the number of rows provided to a component input by a call to the `ProcessInput` method. The log entry includes the component name.|  
|`PipelineBufferLeak`|Provides information about any component that kept buffers alive after the buffer manager goes away. This means that buffers resources were not released and may cause memory leaks. The log entry provides the name of the component and the ID of the buffer.|  
|`PipelineExecutionPlan`|Reports the execution plan of the data flow. It provides information about how buffers will be sent to components. This information, in combination with the PipelineExecutionTrees entry, describes what is occurring in the task.|  
|`PipelineExecutionTrees`|Reports the execution trees of the layout in the data flow. The scheduler of the data flow engine use the trees to build the execution plan of the data flow.|  
|`PipelineInitialization`|Provides initialization information about the task. This information includes the directories to use for temporary storage of BLOB data, the default buffer size, and the number of rows in a buffer. Depending on the configuration of the Data Flow task, multiple log entries may be written.|  
  
###  <a name="ExecuteDTS200"></a> Execute DTS 2000 Task  
 The following table lists the custom log entries for the Execute DTS 2000 task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ExecuteDTS80PackageTaskBegin`|Indicates that the task began to run a DTS 2000 package.|  
|`ExecuteDTS80PackageTaskEnd`|Indicates that the task finished.<br /><br /> Note: The DTS 2000 package may continue to run after the task ends.|  
|`ExecuteDTS80PackageTaskTaskInfo`|Provides descriptive information about the task.|  
|`ExecuteDTS80PackageTaskTaskResult`|Reports the execution result of the DTS 2000 package that the task ran.|  
  
###  <a name="ExecuteProcess"></a> Execute Process Task  
 The following table lists the custom log entries for the Execute Process task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ExecuteProcessExecutingProcess`|Provides information about the process of running the executable that the task is configured to run.<br /><br /> Two log entries are written. One contains information about the name and location of the executable that the task runs, and the other records the exit from the executable.|  
|`ExecuteProcessVariableRouting`|Provides information about which variables are routed to the input and outputs of the executable. Log entries are written for stdin (the input), stdout (the output), and stderr (the error output).|  
  
###  <a name="ExecuteSQL"></a> Execute SQL Task  
 The following table describes the custom log entry for the Execute SQL task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ExecuteSQLExecutingQuery`|Provides information about the execution phases of the SQL statement. Log entries are written when the task acquires connection to the database, when the task starts to prepare the SQL statement, and after the execution of the SQL statement is completed. The log entry for the prepare phase includes the SQL statement that the task uses.|  
  
###  <a name="FileSystem"></a> File System Task  
 The following table describes the custom log entry for the File System task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`FileSystemOperation`|Reports the operation that the task performs. The log entry is written when the file system operation starts and includes information about the source and destination.|  
  
###  <a name="FTP"></a> FTP Task  
 The following table lists the custom log entries for the FTP task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`FTPConnectingToServer`|Indicates that the task initiated a connection to the FTP server.|  
|`FTPOperation`|Reports the beginning of and the type of FTP operation that the task performs.|  
  
###  <a name="MessageQueue"></a> Message Queue Task  
 The following table lists the custom log entries for the Message Queue task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`MSMQAfterOpen`|Indicates that the task finished opening the message queue.|  
|`MSMQBeforeOpen`|Indicates that the task began to open the message queue.|  
|`MSMQBeginReceive`|Indicates that the task began to receive a message.|  
|`MSMQBeginSend`|Indicates that the task began to send a message.|  
|`MSMQEndReceive`|Indicates that the task finished receiving a message.|  
|`MSMQEndSend`|Indicates that the task finished sending a message|  
|`MSMQTaskInfo`|Provides descriptive information about the task.|  
|`MSMQTaskTimeOut`|Indicates that the task timed out.|  
  
###  <a name="Script"></a> Script Task  
 The following table describes the custom log entry for the Script task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`ScriptTaskLogEntry`|Reports the results of implementing logging in the script. A log entry is written for each call to the `Log` method of the `Dts` object. The entry is written when the code is run. For more information, see [Logging in the Script Task](extending-packages-scripting/task/logging-in-the-script-task.md).|  
  
###  <a name="SendMail"></a> Send Mail Task  
 The following table lists the custom log entries for the Send Mail task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`SendMailTaskBegin`|Indicates that the task began to send an e-mail message.|  
|`SendMailTaskEnd`|Indicates that the task finished sending an e-mail message.|  
|`SendMailTaskInfo`|Provides descriptive information about the task.|  
  
###  <a name="TransferDatabase"></a> Transfer Database Task  
 The following table lists the custom log entries for the Transfer Database task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`SourceDB`|Specifies the database that the task copied.|  
|`SourceSQLServer`|Specifies the computer from which the database was copied.|  
  
###  <a name="TransferErrorMessages"></a> Transfer Error Messages Task  
 The following table lists the custom log entries for the Transfer Error Messages task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`TransferErrorMessagesTaskFinishedTransferringObjects`|Indicates that the task finished transferring error messages.|  
|`TransferErrorMessagesTaskStartTransferringObjects`|Indicates that the task started to transfer error messages.|  
  
###  <a name="TransferJobs"></a> Transfer Jobs Task  
 The following table lists the custom log entries for the Transfer Jobs task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`TransferJobsTaskFinishedTransferringObjects`|Indicates that the task finished transferring [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent jobs.|  
|`TransferJobsTaskStartTransferringObjects`|Indicates that the task started to transfer [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent jobs.|  
  
###  <a name="TransferLogins"></a> Transfer Logins Task  
 The following table lists the custom log entries for the Transfer Logins task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`TransferLoginsTaskFinishedTransferringObjects`|Indicates that the task finished transferring logins.|  
|`TransferLoginsTaskStartTransferringObjects`|Indicates that the task started to transfer logins.|  
  
###  <a name="TransferMasterStoredProcedures"></a> Transfer Master Stored Procedures Task  
 The following table lists the custom log entries for the Transfer Master Stored Procedures task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`TransferStoredProceduresTaskFinishedTransferringObjects`|Indicates that the task finished transferring user-defined stored procedures that are stored in the **master** database.|  
|`TransferStoredProceduresTaskStartTransferringObjects`|Indicates that the task started to transfer user-defined stored procedures that are stored in the **master** database.|  
  
###  <a name="TransferSQLServerObjects"></a> Transfer SQL Server Objects Task  
 The following table lists the custom log entries for the Transfer [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Objects task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`TransferSqlServerObjectsTaskFinishedTransferringObjects`|Indicates that the task finished transferring [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database objects.|  
|`TransferSqlServerObjectsTaskStartTransferringObjects`|Indicates that the task started to transfer [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database objects.|  
  
###  <a name="WebServices"></a> Web Services Task  
 The following table lists the custom log entries that you can enable for the Web Services task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`WSTaskBegin`|The task began to access a Web service.|  
|`WSTaskEnd`|The task completed a Web service method.|  
|`WSTaskInfo`|Descriptive information about the task.|  
  
###  <a name="WMIDataReader"></a> WMI Data Reader Task  
 The following table lists the custom log entries for the WMI Data Reader task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`WMIDataReaderGettingWMIData`|Indicates that the task began to read WMI data.|  
|`WMIDataReaderOperation`|Reports the WQL query that the task ran.|  
  
###  <a name="WMIEventWatcher"></a> WMI Event Watcher Task  
 The following table lists the custom log entries for the WMI Event Watcher task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`WMIEventWatcherEventOccurred`|Denotes that the event occurred that the task was monitoring.|  
|`WMIEventWatcherTimedout`|Indicates that the task timed out.|  
|`WMIEventWatcherWatchingForWMIEvents`|Indicates that the task began to execute the WQL query. The entry includes the query.|  
  
###  <a name="XML"></a> XML Task  
 The following table describes the custom log entry for the XML task.  
  
|Log entry|Description|  
|---------------|-----------------|  
|`XMLOperation`|Provides information about the operation that the task performs|  
  
## Related Content  
 Blog entry, [Logging custom events for Integration Services tasks](https://go.microsoft.com/fwlink/?LinkId=150580), on dougbert.com.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)  
  
  
