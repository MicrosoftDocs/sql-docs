---
title: "AMO Other Classes and Methods | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "restores [AMO]"
  - "AMO, backup and restore"
  - "capture logs [AMO]"
  - "AmoException class [AMO]"
  - "Analysis Management Objects, backup and restore"
  - "assembly objects [AMO]"
  - "traces [AMO]"
  - "backups [AMO]"
ms.assetid: 60ed5cfa-3a03-4161-8271-0a71a3ae363b
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AMO Other Classes and Methods
  This section contains common classes that are not specific to OLAP or data mining, and that are helpful when administering or managing objects in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. These classes cover features such as stored procedures, tracing, exceptions, and backup and restore.  
  
 This topic contains the following sections:  
  
-   [Assembly Objects](#Assembly)  
  
-   [Backup and Restore Methods](#Backup)  
  
-   [Trace Objects](#Traces)  
  
-   [CaptureLog Class and CaptureXML Attribute](#CaptureLog)  
  
-   [AMOException Exception Class](#AMO)  
  
 The following illustration shows the relationship of the classes that are explained in this topic.  
  
 ![AMO Other Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/media/amo-otherclasses.gif "AMO Other Classes")  
  
##  <a name="Assembly"></a> Assembly Objects  
 An <xref:Microsoft.AnalysisServices.Assembly> object is created by adding it to the assemblies collection of the server, and then updating the <xref:Microsoft.AnalysisServices.Assembly> object to the server, by using the Update method.  
  
 To remove an <xref:Microsoft.AnalysisServices.Assembly> object, it has to be dropped by using the Drop method of the <xref:Microsoft.AnalysisServices.Assembly> object. Removing an <xref:Microsoft.AnalysisServices.Assembly> object from the assemblies collection of the database does not drop the assembly, it only prevents you from seeing it in your application until the next time that you run your application.  
  
 For more information about methods and properties available, see <xref:Microsoft.AnalysisServices.Assembly> in <xref:Microsoft.AnalysisServices> .  
  
> [!IMPORTANT]  
>  COM assemblies might pose a security risk. Due to this risk and other considerations, COM assemblies were deprecated in [!INCLUDE[ssASversion10](../../../includes/ssasversion10-md.md)]. COM assemblies might not be supported in future releases.  
  
##  <a name="Backup"></a> Backup and Restore Methods  
 Backup and Restore are methods that can be used to create copies of an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database and recover the database by using the copy. The Backup method belongs to the <xref:Microsoft.AnalysisServices.Database> object, and the Restore method belongs to the <xref:Microsoft.AnalysisServices.Server> object.  
  
 Only server and database administrators are permitted to perform a backup of a database. Only server administrators can restore a database onto a different server than it was backed up from. Database administrators can restore a database by overwriting the existing database only if they own the database that is to be overwritten. After a restore, the database administrator may lose access to the restored database if the database is restored with its original security definitions.  
  
 Database backup files must have .abf extensions.  
  
### Backup Method  
 To backup a database, use the Backup method of the database object with the name of the backup file as a parameter.  
  
##### Default values:  
 AllowOverwrite=**false**  
  
 BackupRemotePartitions=**false**  
  
 Security=**CopyAll**  
  
 ApplyCompression=**true**  
  
### Restore Method  
 To restore a database to a server, use the Restore method of the server with the backup file as a parameter.  
  
##### Default values:  
 AllowOverwrite=**false**  
  
 DataSourceType=**Remote**  
  
 Security=**CopyAll**  
  
##### Restrictions  
  
1.  A local partition cannot be restored as a remote partition.  
  
2.  A remote partition cannot be restored as a local partition, but a remote partition be restored on a different server than it was backed up from.  
  
### Common Parameters and Properties for Backup and Restore Methods  
  
-   **File** is the name of the file to backup (UNC name) into/from.  
  
-   **Location** specifies server-specific backup information, such as **BackupFile**.This allows you to specify a separate backup file for a remote database.  
  
-   **DatasourceID** specifies the ID of the subordinate database in a remote server.  
  
-   **ConnectionString** allows you to adjust the remote datasource in case the remote server has changed. DatasourceID must always be specified when ConnectionString is present.  
  
-   **Folder** allows remapping of the folders for partitions on the local hard drive  
  
-   **Original** is the original folder for local partitions.  
  
-   **New** is the new location for local partitions that used to reside in the corresponding 'Original' old folder.  
  
-   **Password**, if non-blank, specifies that the server will encrypt the backup file.  
  
##  <a name="Traces"></a> Trace Objects  
 Trace is a framework used for monitoring, replaying, and managing an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. A client application, like [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)], subscribes to a trace and the server sends back trace events as specified in the trace definition.  
  
 Each event is described by an event class. The event class describes the type of event generated. Within an event class, event subclasses describe a finer level of categorization. Each event is described by a number of columns. The columns that describe a trace event are consistent for all events and conform to the SQL trace structure. Information recorded in each column may differ depending on the event class; that is, a predefined set of columns is defined for each trace, but the meaning of the column may differ depending on the event class. For example, the TextData column is used to record the original ASSL for all statement events.  
  
 A trace definition can include one or more event classes to be traced concurrently. For each event class, one or more data columns can be added to the trace definition, but not all trace columns must be used. The database administrator can decide which of the available columns to include in a trace. Further, event classes can be selectively traced based on filter criteria on any column in the trace.  
  
 Traces can be started and deleted. Multiple traces can be run at any one time. Trace events can be captured live or directed to a file for later analysis or replay. [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] is the tool used to analyze and replay [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] trace events. Multiple connections are allowed to receive events from the same trace.  
  
 Traces can be divided in two groups: server traces and session traces. Server traces will inform of all events in the server; session traces will inform only events in the current session.  
  
 Traces, from the traces collection of the server, are defined the following way:  
  
1.  Create a <xref:Microsoft.AnalysisServices.Trace> object and populate its basic data, including trace ID, name, log file name, append|overwrite, and others.  
  
2.  Add Events to be monitored to the Events collection of the trace object. For each event, data columns are added.  
  
3.  Set Filters to exclude unnecessary rows of data by adding them to the filters collection.  
  
4.  Start the trace; creating the trace does not start collecting data.  
  
5.  Stop the trace.  
  
6.  Review the trace file with [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)].  
  
 Traces, from the session object, are obtained in the following manner:  
  
1.  Define functions to handle the trace events generated in your application by SessionTrace. Possible events are OnEvent and Stopped.  
  
2.  Add your defined functions to the event handler.  
  
3.  Start the session trace.  
  
4.  Do your process and let your function handlers capture the events.  
  
5.  Stop the session trace.  
  
6.  Continue with your application.  
  
##  <a name="CaptureLog"></a> CaptureLog Class and CaptureXML Attribute  
 All actions to be executed by AMO are sent to the server as XMLA messages. AMO provides the means to capture all these messages without the SOAP headers. For more information, see [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md). CaptureLog is the mechanism in AMO for scripting out objects and operations; objects and operations will be scripted in XMLA.  
  
 To start capturing the XML, the CaptureXML server object property needs to be set to **true**. Then all actions that are to be sent to the server will start being captured in the CaptureLog class, without the actions being sent to the server. CaptureLog is considered a class because it has a method, Clear, which is used to clear the capture log.  
  
 To read the log, you get the strings collection and start iterating over the strings. Also, you can concatenate all logs into a string by using the server object method ConcatenateCaptureLog. ConcatenateCaptureLog requires has three parameters, two of which are required. The required parameters are *transactional*, of Boolean type, and *parallel*, of Boolean type. If *transactional* is set to **true**, it indicates that the XML batch file will be created as a single transaction instead of each command being treated as a separated transaction. If *parallel* is set to **true**, it indicates that all commands in the batch file will be recorded for concurrent execution instead of sequentially as they were recorded.  
  
##  <a name="AMO"></a> AMOException Exception Class  
 You can use AMOException exception class to easily catch exceptions in your application that are thrown by AMO.  
  
 AMO will throw exceptions at different problems found. The following table lists the kind of exceptions that are handled by AMO. Exceptions are derived from the <xref:Microsoft.AnalysisServices.AmoException> class.  
  
|Exception|Origin|Description|  
|---------------|------------|-----------------|  
|<xref:Microsoft.AnalysisServices.AmoException>|Base class|Application receives this exception when a required parent object is missing, or when a requested item is not found in a collection.|  
|<xref:Microsoft.AnalysisServices.OutOfSyncException>|Derived from AMOException|Application receives this exception when AMO is out of synchronization with the engine and the engine returns an object reference that AMO does not know about.|  
|<xref:Microsoft.AnalysisServices.OperationException>|Derived from AMOException|This an important exception that is frequently received by applications. This exception contains the details of an error coming from the server, probably because of a faulty AMO operation like Update or Process or Drop.|  
|<xref:Microsoft.AnalysisServices.ResponseFormatException>|Derived from AMOException|This exception occurs when the engine returns a message in a format that AMO does not understand.|  
|<xref:Microsoft.AnalysisServices.ConnectionException>|Derived from AMOException|This exception occurs when a connection cannot be established (with Server.Connect) or when the connection is lost while AMO is communicating with the engine (for example, during an Update or Process or Drop).|  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)  
  
  