---
title: "QN:Dynamics Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "event classes [SQL Server], QN:Dynamics"
ms.assetid: 3c1ffa0c-c9e5-40a6-a26b-28339f60ebc3
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# QN:Dynamics Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The QN:Dynamics event class reports information about the background activity that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] performs to support query notifications. Within the [!INCLUDE[ssDE](../../includes/ssde-md.md)], a background thread monitors subscription time-outs, pending subscriptions to be fired, and parameter table destruction.  
  
## QN:Dynamics Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|ApplicationName|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|**int**|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the Server Name data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|The name of the database in which the user statement is running.|35|Yes|  
|EventClass|**int**|Type of event = 202|27|No|  
|EventSequence|**int**|Sequence number for this event.|51|No|  
|EventSubClass|**nvarchar**|The type of event subclass, providing further information about each event class. This column may contain the following values:<br /><br /> **Clock run started**: Indicates that the background thread in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that schedules expired parameter tables for cleanup has started.<br /><br /> **Clock run finished**: Indicates that the background thread in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that schedules expired parameter tables for cleanup has finished.<br /><br /> **Master cleanup task started**: Indicates when cleanup (garbage collection) to remove expired query notification subscription data starts.<br /><br /> **Master cleanup task finished**: Indicates when cleanup (garbage collection) to remove expired query notification subscription data finishes.<br /><br /> **Master cleanup task skipped**: Indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] did not perform cleanup (garbage collection) to remove expired query notification subscription data.|21|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process.<br /><br /> 0 = user<br /><br /> 1 = system|60|No|  
|LoginName|**nvarchar**|The name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of *DOMAIN\Username*).|11|No|  
|LoginSID|**image**|The security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|RequestID|**int**|Identifier of the request that contains the statement.|49|Yes|  
|ServerName|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user that originated the session. For example, if an application connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and executes a statement as Login2, SessionLoginName shows "Login1" and LoginName shows "Login2". This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Returns an XML document containing information specific to this event. This document conforms to the XML schema available at the [SQL Server Query Notification Profiler Event Schema](https://go.microsoft.com/fwlink/?LinkId=63331) page.|1|Yes|  
  
  
