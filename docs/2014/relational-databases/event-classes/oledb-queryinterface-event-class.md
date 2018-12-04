---
title: "OLEDB QueryInterface Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "OLEDB QueryInterface event class"
ms.assetid: f54c9ef9-3add-497c-a09b-42c4ce3c623d
author: stevestein
ms.author: sstein
manager: craigg
---
# OLEDB QueryInterface Event Class
  The **OLEDB QueryInterface** event class occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] issues an OLE DB **QueryInterface** call for distributed queries and remote stored procedures. Include this event class in traces that are monitoring problems associated with distributed queries and remote stored procedures.  
  
 When the **OLEDB QueryInterface** event class is included, the amount of overhead will be high. If such events occur frequently, the trace may significantly impede performance. To minimize overhead incurred, limit use of this event class to traces that monitor specific problems for brief periods of time.  
  
## OLEDB QueryInterface Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|Duration|`bigint`|Length of time to complete the OLE DB QueryInterface event.|13|No|  
|EndTime|`datetime`|Time the event ended.|15|Yes|  
|Error|`int`|Error number of a given event. Often this is the error number stored in the **sys.messages** catalog view.|31|Yes|  
|EventClass|`int`|Type of event = 120.|27|No|  
|EventSequence|`int`|Sequence of the OLE DB event class in the batch.|51|No|  
|EventSubClass|`int`|0=Starting<br /><br /> 1=Completed|21|No|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LinkedServerName|`nvarchar`|Name of the linked server.|45|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|`image`|Security identifier (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|MethodName|`nvarchar`|Name of the calling method.|47|No|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|ProviderName|`nvarchar`|Name of the OLE DB provider.|46|Yes|  
|RequestID|`int`|ID of the request containing the statement.|49|Yes|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, `SessionLoginName` shows Login1 and `LoginName` shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TextData|`nvarchar`|Parameters sent and received in the OLE DB call.|1|No|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [Extended Events](../extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)   
 [OLE Automation Objects in Transact-SQL](../stored-procedures/ole-automation-objects-in-transact-sql.md)  
  
  
