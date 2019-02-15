---
title: "Object:Altered Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Object:Altered event class"
ms.assetid: f94e3b59-ff2f-4d8d-8479-e85ce5b3483e
author: stevestein
ms.author: sstein
manager: craigg
---
# Object:Altered Event Class
  The Object:Altered event class indicates that an object has been altered; for example, by an ALTER INDEX, ALTER TABLE, or ALTER DATABASE statement. This event class can be used to determine if objects are being altered; for example, by ODBC applications, which often create temporary stored procedures.  
  
 The Object:Altered event class always occurs as two events. The first event indicates the Begin phase. The second event indicates the Rollback or Commit phase.  
  
 By monitoring the LoginName and NTUserName data columns, you can determine the name of the user who is creating, deleting, or altering objects.  
  
## Object:Altered Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|`int`|Type of event = 164.|27|No|  
|EventSequence|`int`|Sequence of a given event within the request.|51|No|  
|EventSubClass|`int`|Type of event subclass.<br /><br /> 0=Begin<br /><br /> 1=Commit<br /><br /> 2=Rollback|21|Yes|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IndexID|`int`|ID for the index on the object affected by the event. To determine the index ID for an object, use the index_id column of the sys.indexes catalog view.|24|Yes|  
|IntegerData|`int`|Event sequence number of the corresponding Begin event. This column is available only for the Commit or Rollback type of event. subclass.|25|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, NULL = user.|60|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|`image`|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|ObjectID|`int`|System-assigned ID of the object.|22|Yes|  
|ObjectID2|`bigint`|Partition function ID when the partition schema is altered, the queue ID when service is altered or the collection schema ID when XML schema is altered.|56|Yes|  
|ObjectName|`nvarchar`|Name of the object being referenced.|34|Yes|  
|ObjectType|`int`|Value representing the type of the object involved in the event. This value corresponds to the type column in the sy.sobjects catalog view. For values, see [ObjectType Trace Event Column](objecttype-trace-event-column.md).|28|Yes|  
|RequestID|`int`|ID of the batch request containing the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|`bigint`|Token that describes the current transaction.|50|Yes|  
  
## See Also  
 [Extended Events](../extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
