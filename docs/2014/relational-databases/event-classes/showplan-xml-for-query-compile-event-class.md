---
title: "Showplan XML for Query Compile Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Showplan XML For Query Compile event class"
ms.assetid: 48919fcb-3a22-43ca-a63c-b210cf2c32d5
author: stevestein
ms.author: sstein
manager: craigg
---
# Showplan XML for Query Compile Event Class
  The Showplan XML For Query Compile event class occurs when [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compiles an SQL statement. Include thisevent class to identify the Showplan operators on [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Showplan XML For Query Compile event class displays complete, compile time data, so traces that contain this event class can incur significant performance overhead. To minimize this, limit use of this event class to traces that monitor specific problems for brief periods of time.  
  
 The Showplan XML documents have a schema associated with them. This schema can be found at the [Microsoft Web Site](https://go.microsoft.com/fwlink/?LinkId=41740), or as part of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
  
## Showplan XML for Query Compile Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BinaryData|`image`|Estimated cost of the query.|2|No|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|Event Class|`int`|Type of Event = 168.|27|No|  
|EventSequence|`int`|Sequence of a given event within the request.|51|No|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData|`int`|Estimated number of rows returned.|25|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LineNumber|`int`|Displays the number of the line containing the error.|5|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSID|`image`|The security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|No|  
|NestLevel|`int`|Integer representing the data returned by @@NESTLEVEL.|29|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|ObjectID|`int`|System-assigned ID of the object.|22|Yes|  
|ObjectName|`nvarchar`|The name of the object being referenced.|34|Yes|  
|ObjectType|`int`|Value representing the type of the object involved in the event. This value corresponds to the type column in sys.objects. For values, see [ObjectType Trace Event Column](objecttype-trace-event-column.md).|28|Yes|  
|RequestID|`int`|ID of the request containing the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TextData|`ntext`|Text value dependent on the event class captured in the trace.|1|Yes|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|`bigint`|Token used to describe the current transaction.|50|Yes|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)   
 [Showplan Logical and Physical Operators Reference](../showplan-logical-and-physical-operators-reference.md)  
  
  
