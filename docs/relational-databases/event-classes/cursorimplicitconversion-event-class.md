---
title: "CursorImplicitConversion Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "CursorImplicitConversion event class"
ms.assetid: 44d12e23-146a-42e6-bb38-1f2f6a035bad
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CursorImplicitConversion Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **CursorImplicitConversion** event class describes cursor-implicit conversion events that occur in application programming interfaces (APIs) or [!INCLUDE[tsql](../../includes/tsql-md.md)] cursors. Cursor implicit conversion events occur when the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] executes a Transact-SQL statement that is not supported by server cursors of the type requested. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] returns an error that indicates the cursor type has changed.  
  
 Include the **CursorImplicitConversion** event class in traces that are recording the performance of cursors.  
  
 When this event class is included in a trace, the amount of overhead incurred depends on how frequently cursors that require implicit conversion are used against the database during the trace. If cursors are used extensively, the trace may significantly impede performance.  
  
## CursorImplicitConversion Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**ApplicationName**|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**BinaryData**|**image**|Resulting cursor type. Values are:<br /><br /> 1 = Keyset<br /><br /> 2 = Dynamic<br /><br /> 4 = Forward only<br /><br /> 8 = Static<br /><br /> 16 = Fast forward|2|Yes|  
|**ClientProcessID**|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|**DatabaseID**|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**EventClass**|**int**|Type of event recorded = 76.|27|No|  
|**EventSequence**|**int**|Sequence of the **CursorClose** event class in the batch.|51|No|  
|**GroupID**|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|**Handle**|**int**|Handle of the object referenced in the event.|33|Yes|  
|**HostName**|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IntegerData**|**int**|Requested cursor type. Values are:<br /><br /> 1 = Keyset<br /><br /> 2 = Dynamic<br /><br /> 4 = Forward only<br /><br /> 8 = Static<br /><br /> 16 = Fast forward|25|No|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginName**|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|**LoginSid**|**image**|Security identifier (SID) of the logged-in user. You can find this information in the **sys.server_principals** catalog view. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|Windows user name.|6|Yes|  
|**RequestID**|**int**|Request identifier of the implicit conversion.|49|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user that originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|**XactSequence**|**bigint**|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
