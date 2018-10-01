---
title: "SQL:StmtRecompile Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL:StmtRecompile event class"
ms.assetid: 3a134751-3e93-4fe8-bf22-1e0561189293
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL:StmtRecompile Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The SQL:StmtRecompile event class indicates statement-level recompilations caused by all types of batches: stored procedures, triggers, ad hoc batches, and queries. Queries can be submitted by using sp_executesql, dynamic SQL, Prepare methods, Execute methods, or similar interfaces. The SQL:StmtRecompile event class should be used instead of the SP:Recompile event class.  
  
## SQL:StmtRecompile Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program|10|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database in which the stored procedure is running. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the stored procedure is running.|35|Yes|  
|EventSequence|**int**|The sequence of an event within the request.|51|No|  
|EventSubClass|**int**|Describes the cause of the recompilation:<br /><br /> 1 = Schema changed<br /><br /> 2 = Statistics changed<br /><br /> 3 = Deferred compile<br /><br /> 4 = Set option changed<br /><br /> 5 = Temp table changed<br /><br /> 6 = Remote rowset changed<br /><br /> 7 = For Browse permissions changed<br /><br /> 8 = Query notification environment changed<br /><br /> 9 = Partition view changed<br /><br /> 10 = Cursor options changed<br /><br /> 11 = Option (recompile) requested|21|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running which submitted this statement. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData2|**int**|Ending offset of the statement within the stored procedure or batch that caused recompilation. Ending offset is -1 if the statement is the last statement in its batch.|55|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process.<br /><br /> 1 = system<br /><br /> 0 = user|60|Yes|  
|LineNumber|**int**|Sequence number of this statement within the batch, if applicable.|5|Yes|  
|LoginName|**nvarchar**|Name of the login that submitted this batch.|11|Yes|  
|LoginSid|**image**|Security identifier (SID) of the currently logged in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NestLevel|**int**|The nesting level of the stored procedure call. For example, my_proc_a stored procedure calls my_proc_b. In this case, my_proc_a has a NestLevel of 1, my_proc_b has a NestLevel of 2.|29|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name of connected user.|6|Yes|  
|ObjectID|**int**|System-assigned identifier of the object that contains the statement that caused the recompilation. This object can be a stored procedure, trigger, or user-defined function. For ad hoc batches or prepared SQL, ObjectID and ObjectName return a NULL value.|22|Yes|  
|ObjectName|**nvarchar**|Name of the object identified by ObjectID.|34|Yes|  
|ObjectType|**int**|Value that represents the type of object involved in the event. For more information, see [ObjectType Trace Event Column](../../relational-databases/event-classes/objecttype-trace-event-column.md).|28|Yes|  
|Offset|**int**|Starting offset of the statement within the stored procedure or batch that caused recompilation.|61|Yes|  
|RequestID|**int**|ID of the request containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|Server process ID of the connection.|12|Yes|  
|SqlHandle|**varbinary**|64-bit hash based on the text of an ad hoc query or the database and object ID of an SQL object. This value can be passed to sys.dm_exec_sql_text to retrieve the associated SQL text.|63|No|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Text of the Transact-SQL statement that recompiled.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|**bigint**|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [SP:Recompile Event Class](../../relational-databases/event-classes/sp-recompile-event-class.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
