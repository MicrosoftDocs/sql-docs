---
title: SQL Trace
description: SQL Trace
ms.service: sql
ms.subservice: 
ms.topic: conceptual
ms.assetid: 83c6d1d9-19ce-43fe-be9a-45aaa31f20cb
author: MashaMSFT
ms.author: mathoma
ms.reviewer: ""
ms.custom: ""
ms.date: 11/27/2018
---

# SQL Trace

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
In SQL Trace, events are gathered if they are instances of event classes listed in the trace definition. These events can be filtered out of the trace or queued for their destination. The destination can be a file or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO), which can use the trace information in applications that manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

> [!IMPORTANT]
> SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects are also deprecated. 
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] 
> Use Extended Events instead. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

## Benefits of SQL Trace  
Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides [!INCLUDE[tsql](../../includes/tsql-md.md)] system stored procedures to create traces on an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. These system stored procedures can be used from within your own applications to create traces manually, instead of using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. This allows you to write custom applications specific to the needs of your enterprise.  
  
## SQL Trace Architecture  
Event Sources can be any source that produces the trace event, such as [!INCLUDE[tsql](../../includes/tsql-md.md)] batches or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events, such as deadlocks. For more information about events, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md). After an event occurs, if the event class has been included in a trace definition, the event information is gathered by the trace. If filters have been defined for the event class in the trace definition, the filters are applied and the trace event information is passed to a queue. From the queue, the trace information is either written to a file or can be used by SMO in applications, such as [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. The following diagram shows how SQL Trace gathers events during a tracing.  
  
![Database Engine event tracing process](../../relational-databases/sql-trace/media/tracarch.gif "Database Engine event tracing process")  
  
## SQL Trace Terminology  
The following terms describe the key concepts of SQL Trace.  
  
 **Event**  
 The occurrence of an action within an instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
 **Data column**  
 An attribute of an event.  
  
 **Event class**  
 A type of event that can be traced. The event class contains all of the data columns that can be reported by an event.  
  
 **Event category**  
 A group of related event classes.  
  
 **Trace** (noun)  
 A collection of events and data returned by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 **Trace** (verb)  
 To collect and monitor events in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Tracedefinition**  
 A collection of event classes, data columns and filters that identify the types of events to be collected during a trace.  
  
 **Filter**  
 Criteria that limit the events that are collected in a trace.  
  
 **Trace file**  
 A file created when a trace is saved.  
  
 **Template**  
 In [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], a file that defines the event classes and data columns to be collected in a trace.  
  
 **Trace table**  
 In [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], a table that is created when a trace is saved to a table.  
  
## Use Data Columns to Describe Returned Events  
SQL Trace uses data columns in the trace output to describe events that are returned when the trace runs. The following table describes the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] data columns, which are the same data columns as those used by SQL Trace, and indicates the columns that are selected by default.  
  
|Data column|Column number|Description|  
|-----------------|-------------------|-----------------|  
|\* **ApplicationName**|10|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application and not the name of the program.|  
|**BigintData1**|52|Value (**bigint** data type), which depends on the event class specified in the trace.|  
|**BigintData2**|53|Value (**bigint** data type), which depends on the event class specified in the trace.|  
|\* **Binary Data**|2|The binary value dependent on the event class that is captured in the trace.|  
|\* **ClientProcessID**|9|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|  
|**ColumnPermissions**|44|Indicates whether a column permission was set. You can parse the statement text to determine which permissions were applied to which columns.|  
|\* **CPU**|18|The amount of CPU time (in milliseconds) that is used by the event.|  
|**Database ID**|3|The ID of the database specified by the USE *database_name* statement, or the ID of the default database if no USE *database_name*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|  
|**DatabaseName**|35|The name of the database in which the user statement is running.|  
|**DBUserName**|40|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user name of the client.|  
|\* **Duration**|13|The duration (in microseconds) of the event.<br /><br /> The server reports the duration of an event in microseconds (one millionth, or 10<sup>-6</sup>, of a second) and the amount of CPU time used by the event in milliseconds (one thousandth, or 10<sup>-3</sup>, of a second). The [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] graphical user interface displays the **Duration** column in milliseconds by default, but when a trace is saved to either a file or a database table, the **Duration** column value is written in microseconds.|  
|\* **EndTime**|15|The time at which the event ended. This column is not populated for event classes that refer to an event that is starting, such as **SQL:BatchStarting** or **SP:Starting**.|  
|**Error**|31|The error number of a given event. Often this is the error number stored in **sysmessages**.|  
|\* **EventClass**|27|The type of event class that is captured.|  
|**EventSequence**|51|Sequence number for this event.|  
|**EventSubClass**|21|The type of event subclass, which provides further information about each event class. For example, event subclass values for the **Execution Warning** event class represent the type of execution warning:<br /><br /> **1** = Query wait. The query must wait for resources before it can execute; for example, memory.<br /><br /> **2** = Query time-out. The query timed out while waiting for required resources to execute. This data column is not populated for all event classes.|  
|**GUID**|54|GUID value which depends on the event class specified in the trace.|  
|**FileName**|36|The logical name of the file that is modified.|  
|**Handle**|33|The integer used by ODBC, OLE DB, or DB-Library to coordinate server execution.|  
|**HostName**|8|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|  
|**IndexID**|24|The ID for the index on the object affected by the event. To determine the index ID for an object, use the **indid** column of the **sysindexes** system table.|  
|**IntegerData**|25|The integer value dependent on the event class captured in the trace.|  
|**IntegerData2**|55|The integer value dependent on the event class captured in the trace.|  
|**IsSystem**|60|Indicates whether the event occurred on a system process or a user process:<br /><br /> **1** = system<br /><br /> **0** = user|  
|**LineNumber**|5|Contains the number of the line that contains the error. For events that involve [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, like **SP:StmtStarting**, the **LineNumber** contains the line number of the statement in the stored procedure or batch.|  
|**LinkedServerName**|45|Name of the linked server.|  
|\* **LoginName**|11|The name of the login of the user (either SQL Server security login or the Windows login credentials in the form of DOMAIN\Username).|  
|**LoginSid**|41|The security identifier (SID) of the logged-in user. You can find this information in the **sys.server_principals** view of the **master** database. Each login to the server has a unique ID.|  
|**MethodName**|47|Name of the OLEDB method.|  
|**Mode**|32|The integer used by various events to describe a state the event is requesting or has received.|  
|**NestLevel**|29|The integer that represents the data returned by @@NESTLEVEL.|  
|**NTDomainName**|7|The Microsoft Windows domain to which the user belongs.|  
|\* **NTUserName**|6|The Windows user name.|  
|**ObjectID**|22|The system-assigned ID of the object.|  
|**ObjectID2**|56|The ID of the related object or entity, if available.|  
|**ObjectName**|34|The name of the object that is referenced.|  
|\*\***ObjectType**|28|The value representing the type of the object involved in the event. This value corresponds to the **type** column in **sysobjects**.|  
|**Offset**|61|The starting offset of the statement within the stored procedure or batch.|  
|**OwnerID**|58|For lock events only. The type of the object that owns a lock.|  
|**OwnerName**|37|The database user name of the object owner.|  
|**ParentName**|59|The name of the schema in which the object resides.|  
|**Permissions**|19|The integer value that represents the type of permissions checked. Values are:<br /><br /> **1** = SELECT ALL<br /><br /> **2** = UPDATE ALL<br /><br /> **4** = REFERENCES ALL<br /><br /> **8** = INSERT<br /><br /> **16** = DELETE<br /><br /> **32** = EXECUTE (procedures only)<br /><br /> **4096** = SELECT ANY (at least one column)<br /><br /> **8192** = UPDATE ANY<br /><br /> **16384** = REFERENCES ANY|  
|**ProviderName**|46|Name of the OLEDB provider.|  
|\* **Reads**|16|The number of read operations on the logical disk that are performed by the server on behalf of the event. These read operations include all reads from tables and buffers during the statement's execution.|  
|**RequestID**|49|ID of the request that contains the statement.|  
|**RoleName**|38|The name of the application role that is being enabled.|  
|**RowCounts**|48|The number of rows in the batch.|  
|**ServerName**|26|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being traced.|  
|**SessionLoginName**|64|The login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using **Login1** and execute a statement as **Login2**, **SessionLoginName** displays **Login1**, while **LoginName** displays **Login2**. This data column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|  
|**Severity**|20|The severity level of the exception event.|  
|**SourceDatabaseID**|62|The ID of the database in which the source of the object exists.|  
|\* **SPID**|12|The server process ID (SPID) that is assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process that is associated with the client.|  
|**SqlHandle**|63|64-bit hash based on the text of an ad hoc query or the database and object ID of an SQL object. This value can be passed to **sys.dm_exec_sql_text()** to retrieve the associated SQL text.|  
|\* **StartTime**|14|The time at which the event started, when available.|  
|**State**|30|Error state code.|  
|**Success**|23|Represents whether the event was successful. Values include:<br /><br /> **1** = Success.<br /><br /> **0** = Failure<br /><br /> For example, a **1** means a successful permissions check, and a **0** means a failed check.|  
|**TargetLoginName**|42|For actions that target a login, the name of the targeted login; for example, to add a new login.|  
|**TargetLoginSid**|43|For actions that target a login, the SID of the targeted login; for example, to add a new login.|  
|**TargetUserName**|39|For actions that target a database user, the name of that user; for example, to grant permission to a user.|  
|\* **TextData**|1|The text value dependent on the event class that is captured in the trace. However, if you trace a parameterized query, the variables are not displayed with data values in the **TextData** column.|  
|**Transaction ID**|4|The system-assigned ID of the transaction.|  
|**Type**|57|The integer value dependent on the event class captured in the trace.|  
|\* **Writes**|17|The number of physical disk write operations that are performed by the server on behalf of the event.|  
|**XactSequence**|50|A token to describe the current transaction.|  

\* These data columns are populated by default for all events.

\*\* For more information about the **ObjectType** data column, see [ObjectType Trace Event Column](../../relational-databases/event-classes/objecttype-trace-event-column.md).

## SQL Trace Tasks
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to create and run traces using Transact-SQL stored procedures.|[Create and Run Traces Using Transact-SQL Stored Procedures](../../relational-databases/sql-trace/create-and-run-traces-using-transact-sql-stored-procedures.md)|  
|Describes how to create manual traces using stored procedures on an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].|[Create Manual Traces using Stored Procedures](../../relational-databases/sql-trace/create-manual-traces-using-stored-procedures.md)|  
|Describes how to save trace results to the file where the trace results are written.|[Save Trace Results to a File](../../relational-databases/sql-trace/save-trace-results-to-a-file.md)|  
|Describes how to improve access to trace data by using space in the **temp** directory.|[Improve Access to Trace Data](../../relational-databases/sql-trace/improve-access-to-trace-data.md)|  
|Describes how to use stored procedures to create a trace.|[Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md)|  
|Describes how to use stored procedures to create a filter that retrieves only the information you need on an event being traced.|[Set a Trace Filter &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/set-a-trace-filter-transact-sql.md)|  
|Describes how to use stored procedures to modify an existing trace.|[Modify an Existing Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/modify-an-existing-trace-transact-sql.md)|  
|Describes how to use built-in functions to view a saved trace.|[View a Saved Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/view-a-saved-trace-transact-sql.md)|  
|Describes how to use built-in functions to view trace filter information.|[View Filter Information &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/view-filter-information-transact-sql.md)|  
|Describes how to use stored procedures to delete a trace.|[Delete a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/delete-a-trace-transact-sql.md)|  
|Describes how to minimize the performance cost incurred by a trace.|[Optimize SQL Trace](../../relational-databases/sql-trace/optimize-sql-trace.md)|  
|Describes how to filter a trace to minimize the overhead that is incurred during a trace.|[Filter a Trace](../../relational-databases/sql-trace/filter-a-trace.md)|  
|Describes how to minimize the amount of data that the trace collects.|[Limit Trace File and Table Sizes](../../relational-databases/sql-trace/limit-trace-file-and-table-sizes.md)|  
|Describes the two ways to schedule tracing in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|[Schedule Traces](../../relational-databases/sql-trace/schedule-traces.md)|  
  
## See Also  
 [SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Management Objects &#40;SMO&#41; Programming Guide](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md)  
  
  
