---
description: "SP:Recompile Event Class"
title: "SP:Recompile Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "SP:Recompile event class"
ms.assetid: 526c8eae-a07b-4d0e-b91e-8e537835d77d
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SP:Recompile Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The SP:Recompile event class indicates that a stored procedure, trigger, or user-defined function has been recompiled. Recompilations reported by this event class occur at the statement level.  
  
 The preferred way to trace statement-level recompilations is to use the SQL:StmtRecompile event class. The SP:Recompile event class is deprecated. For more information, see [SQL:StmtRecompile Event Class](../../relational-databases/event-classes/sql-stmtrecompile-event-class.md).  
  
## SP:Recompile Event Class Data Columns  
  
|Data column name|**Data type**|Description|Column ID|Filterable|  
|----------------------|-------------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database in which the stored procedure is running. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the stored procedure is running.|35|Yes|  
|EventClass|**int**|Type of event = 37.|27|No|  
|EventSequence|**int**|The sequence of a given event within the request.|51|No|  
|EventSubClass|**int**|Type of event subclass. Indicates the reason for recompilation.<br /><br /> 1 = Schema Changed<br /><br /> 2 = Statistics Changed<br /><br /> 3 = Recompile DNR<br /><br /> 4 = Set Option Changed<br /><br /> 5 = Temp Table Changed<br /><br /> 6 = Remote Rowset Changed<br /><br /> 7 = For Browse Perms Changed<br /><br /> 8 = Query Notification Environment Changed<br /><br /> 9 = MPI View Changed<br /><br /> 10 = Cursor Options Changed<br /><br /> 11 = With Recompile Option|21|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData2|**int**|Ending offset of the statement within the stored procedure or batch that caused recompilation. Ending offset is -1 if the statement is the last statement in its batch.|55|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NestLevel|**int**|The nesting level of the stored procedure.|29|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|System-assigned ID of the stored procedure.|22|Yes|  
|ObjectName|**nvarchar**|Name of the object that triggered the recompile.|34|Yes|  
|ObjectType|**int**|Value that represents the type of object involved in the event. For more information, see [ObjectType Trace Event Column](../../relational-databases/event-classes/objecttype-trace-event-column.md).|28|Yes|  
|Offset|**int**|Starting offset of the statement within the stored procedure or batch that caused recompilation.|61|Yes|  
|RequestID|**int**|ID of the request containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**varbinary**|64-bit hash based on the text of an ad hoc query or the database and object ID of an SQL object. This value can be passed to sys.dm_exec_sql_text to retrieve the associated SQL text.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Text of the Transact-SQL statement that caused a statement-level recompilation.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|**bigint**|Token used to describe the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [SQL:StmtRecompile Event Class](../../relational-databases/event-classes/sql-stmtrecompile-event-class.md)  
  
  
