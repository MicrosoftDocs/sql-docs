---
description: "Auto Stats Event Class"
title: "Auto Stats Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Auto Stats event class"
ms.assetid: cd613fce-01e1-4d8f-86cc-7ffbf0759f9e
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Auto Stats Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **Auto Stats** event class indicates that an automatic updating of index and column statistics has occurred.  **Auto Stats** also fires when statistics are being loaded for use by the optimizer.
  
## Auto Stats Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**ApplicationName**|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**ClientProcessID**|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|**DatabaseID**|**int**|Identifier of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**Duration**|**bigint**|Amount of time (in microseconds) taken by the event.|13|Yes|  
|**EndTime**|**datetime**|Time at which the event ended.|15|Yes|  
|**Error**|**int**|Error number of a given event. Often this is the error number stored in the **sys.messages** catalog view.|31|Yes|  
|**EventClass**|**int**|Type of event = 58.|27|No|  
|**EventSequence**|**int**|Sequence of a given event within the request.|51|No|  
|**EventSubClass**|**int**|Type of event subclass:<br /><br /> 1: Statistics created/updated synchronously; **TextData** column indicates which statistics and whether they were created or updated<br /><br /> 2: Asynchronous statistics update; job queued.<br /><br /> 3: Asynchronous statistics update; job starting.<br /><br /> 4: Asynchronous statistics update; job completed.|21|Yes|  
|**GroupID**|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|**HostName**|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IndexID**|**int**|ID for the index/statistics entry on the object affected by the event. To determine the index ID for an object, use the **index_id** column of the **sys.indexes** catalog view.|24|Yes|  
|**IntegerData**|**int**|Number of statistics collections that were successfully updated.|25|Yes|  
|**IntegerData2**|**int**|Job sequence number.|55|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginName**|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|**LoginSid**|**image**|Security identification number (SID) of the logged-in user. You can find this information in the **sys.server_principals** catalog view. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|Windows user name.|6|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object.|22|Yes|  
|**RequestID**|**int**|The ID of the request containing the statement.|49|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user that originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**Success**|**int**|0 = error.<br /><br /> 1 = success.<br /><br /> 2 = skipped due to server throttling (MSDE).|23|Yes|  
|**TextData**|**ntext**|Contents of this column depends on whether statistics are updated synchronously (**EventSubClass** 1) or asynchronously (**EventSubClass** 2, 3, or 4):<br /><br /> 1: Lists which statistics were updated/created<br /><br /> 2, 3, or 4: NULL; **IndexID** column is populated with the index/statistics ID for statistics updated.|1|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|**Type**|**int**|Type of job.|57|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
