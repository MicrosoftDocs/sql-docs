---
title: "ExistingConnection Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ExistingConnection event class"
ms.assetid: 3eae548f-61af-4f91-ae6f-af5c8a152543
author: stevestein
ms.author: sstein
manager: craigg
---
# ExistingConnection Event Class
  The ExistingConnection event class indicates the properties of existing user connections when the trace was started. The server raises one ExistingConnection event per existing user connection.  
  
## Existing Connection Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BinaryData|`image`|Binary dump of option flags such as session level settings, including ANSI nulls, ANSI padding, cursor close on commit, null concatenation, and quoted identifiers.|2|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|`int`|The current database ID of the user connection. ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|`int`|Type of event = 17.|27|No|  
|EventSequence|`int`|The sequence of this event within this trace.|51|No|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData|`int`|The network packet size in use for the connection.|25|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, NULL = user. Always NULL for this event.|60|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|`image`|Security identifier (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|RequestID|`int`|The ID of the request containing the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the user opened this connection (login time).|14|Yes|  
|TextData|`ntext`|Set options specific to the connection.|1|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)   
 [Audit Login Event Class](audit-login-event-class.md)  
  
  
