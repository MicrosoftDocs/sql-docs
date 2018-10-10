---
title: "Showplan Text (Unencoded) Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Showplan Text (Unencoded) event class"
ms.assetid: 0aad4563-8caf-4971-92af-55992bc5ff2c
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Showplan Text (Unencoded) Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The Showplan Text (Unencoded) event class occurs when [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes an SQL statement. This event class is the same as the Showplan Text event class, except the event information is formatted as a string rather than as binary data.  
  
 The information included is a subset of the information available in Showplan All, Showplan XML, or Showplan XML Statistics Profile event classes.  
  
 When the Showplan Text (Unencoded) event class is included in a trace, the amount of overhead can significantly impede performance. Showplan Text (Unencoded) will not incur as much overhead as other Showplan event classes. To minimize overhead incurred, limit the use of this event class to traces that monitor specific problems for brief periods of time.  
  
## Showplan Text (Unencoded) Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BinaryData|**image**|Binary value dependent on the event class captured in the trace.|2|Yes|  
|ClientProcessID|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|**int**|Type of event = 68.|27|No|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IntegerData|**int**|Integer value dependent on the event class captured in the trace.|25|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LineNumber|**int**|Displays the number of the line containing the error.|5|Yes|  
|LoginName|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|**image**|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NestLevel|**int**|Integer representing the data returned by @@NESTLEVEL.|29|Yes|  
|NTDomainName|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|Windows user name.|6|Yes|  
|ObjectID|**int**|System-assigned ID of the object.|22|Yes|  
|ObjectName|**nvarchar**|Name of the object being referenced.|34|Yes|  
|ObjectType|**int**|Value representing the type of the object involved in the event. This value corresponds to the type column in the sys.objects catalog view. For values, see [ObjectType Trace Event Column](../../relational-databases/event-classes/objecttype-trace-event-column.md).|28|Yes|  
|RequestID|**int**|ID of the request containing the statement.|49|Yes|  
|ServerName|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Text value dependent on the event class captured in the trace.|1|Yes|  
|TransactionID|**bigint**|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|**bigint**|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)   
 [Showplan All Event Class](../../relational-databases/event-classes/showplan-all-event-class.md)   
 [Showplan XML Event Class](../../relational-databases/event-classes/showplan-xml-event-class.md)   
 [Showplan XML Statistics Profile Event Class](../../relational-databases/event-classes/showplan-xml-statistics-profile-event-class.md)  
  
  
