---
title: "Object:Created Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Object:Created event class"
ms.assetid: 57536924-5e66-4b09-a76d-8fcea2131771
author: stevestein
ms.author: sstein
manager: craigg
---
# Object:Created Event Class
  The Object:Created event class indicates that an object has been created, for example, by the CREATE INDEX, CREATE TABLE, or CREATE DATABASE statements.  
  
 This event class can be used to determine if objects are being created, for example, by ODBC applications that often create temporary stored procedures. By monitoring the LoginName and NTUserName data columns, you can determine the name of the user who is creating, deleting, or accessing objects.  
  
## Object:Created Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|ApplicationName|`nvarchar`|Name of the client application that created the connection to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|`int`|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|`int`|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|Name of the database in which the user statement is running.|35|Yes|  
|EventClass|`int`|Type of event = 46.|27|No|  
|EventSequence|`int`|The sequence of a given event within the request.|51|No|  
|EventSubClass|`int`|Type of event subclass.<br /><br /> 0=Begin<br /><br /> 1=Commit<br /><br /> 2=Rollback|21|Yes|  
|GroupID|`int`|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|`nvarchar`|Name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IndexID|`int`|ID for the index on the object affected by the event. To determine the index ID for an object, use the index_id column of the sys.indexes catalog view.|24|Yes|  
|IntegerData|`int`|Integer value dependent on the event class captured in the trace.|25|Yes|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|LoginName|`nvarchar`|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\username).|11|Yes|  
|LoginSid|`image`|Security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|Windows user name.|6|Yes|  
|ObjectID|`int`|System-assigned ID of the object.|22|Yes|  
|ObjectID2|`bigint`|ID of the related object or entity.|56|Yes|  
|ObjectName|`nvarchar`|Name of the object being referenced.|34|Yes|  
|ObjectType|`int`|Value representing the type of the object involved in the event. This value corresponds to the type column in sys.objects. For values, see [ObjectType Trace Event Column](objecttype-trace-event-column.md).|28|Yes|  
|RequestID|`int`|ID of the request containing the statement.|49|Yes|  
|ServerName|`nvarchar`|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|`nvarchar`|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows as Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|`int`|ID of the session on which the event occurred.|12|Yes|  
|StartTime|`datetime`|Time at which the event started, if available.|14|Yes|  
|TransactionID|`bigint`|System-assigned ID of the transaction.|4|Yes|  
|XactSequence|`bigint`|Token used to describe the current transaction.|50|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
