---
title: "User Error Message Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "User Error Message event class"
ms.assetid: d7594261-ccd9-487c-9678-11875ba57fb7
author: stevestein
ms.author: sstein
manager: craigg
---
# User Error Message Event Class
  The User Error Message event class displays the error message as seen by the user in the case of an error or exception. The error message text appears in the TextData field.  
  
## User Error Message Event Class Data Columns  
  
|Data column name|`Data type`|Description|Column ID|Filterable|  
|----------------------|-------------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|Error|`int`|Error number of a given event. Often this is the error number stored in the sys.messages catalog view.|31|Yes|  
|EventClass|`int`|Type of event = 162.|27|No|  
|EventSequence|`int`|Sequence of a given event within the request.|51|No|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|`image`|Security identifier (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|RequestID|`int`|The ID of the request containing the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|Severity|`int`|Severity level of the error or exception.|20|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|State|`int`|Equivalent to an error state code.|30|Yes|  
|TextData|`ntext`|Text of the error message or exception.|1|Yes|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|`bigint`|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
