---
title: "QN:Parameter Table Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "event classes [SQL Server], QN:Parameter Table"
ms.assetid: 292da1ed-4c7e-4bd2-9b84-b9ee09917724
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# QN:Parameter Table Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The QN:Parameter table event reports information about the operations required to create, keep reference counts for, and drop the internal tables that store parameter information. This event also reports the internal activity to reset the usage count for a parameter table.  
  
## QN:Parameter table Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|ApplicationName|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|ClientProcessID|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|**int**|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the Server Name data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|**nvarchar**|The name of the database in which the user statement is running.|35|Yes|  
|EventClass|**Int**|Type of event = 200.|27|No|  
|EventSequence|**int**|Sequence number for this event.|51|No|  
|EventSubClass|**nvarchar**|The type of event subclass, providing further information about each event class. This column may contain the following values:<br /><br /> **Table created**: Indicates a parameter table has been created in the database.<br /><br /> **Table drop attempt**: Indicates that the database has attempted to automatically drop an unused parameter table to free resources.<br /><br /> **Table drop attempt failed**: Indicates that the database tried to drop an unused parameter table and failed. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will automatically reschedule deletion of the parameter table to free up resources.<br /><br /> **Table dropped**: Indicates that the database successfully dropped a parameter table.<br /><br /> **Table pinned**: Indicates that the parameter table is marked for current usage by internal processing.<br /><br /> **Table unpinned**: Indicates that the parameter table has been unpinned. Internal processing has finished using the table.<br /><br /> **Number of users incremented**: Indicates that the number of query notification subscriptions that reference a parameter table has increased.<br /><br /> **Number of users decremented**: Indicates that the number of query notification subscriptions that reference a parameter table has decreased.<br /><br /> **LRU counter reset**: Indicates that the usage count for the parameter table has been reset.<br /><br /> **Cleanup task started**: Indicates when cleanup for all subscriptions in this parameter table has started. This occurs when the database starts up or when a table underlying the subscriptions of this parameter table is dropped.<br /><br /> **Cleanup task finished**: Indicates when cleanup for all subscriptions in this parameter table has finished.|21|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
|HostName|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IsSystem|**int**|Indicates whether the event occurred on a system process or a user process.<br /><br /> 0 = user<br /><br /> 1 = system|60|No|  
|LoginName|**nvarchar**|The name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the Windows login credentials in the form of *DOMAIN*\\*Username*).|11|No|  
|LoginSID|**image**|The security identification number (SID) of the logged-in user. You can find this information in the sys.server_principals catalog view. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|NTUserName|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|RequestID|**int**|Identifier of the request that contains the statement.|49|Yes|  
|ServerName|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SessionLoginName|**nvarchar**|Login name of the user that originated the session. For example, if an application connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and executes a statement as Login2, SessionLoginName shows "Login1" and LoginName shows "Login2". This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|Returns an XML document containing information specific to this event. This document conforms to the XML schema available at the [SQL Server Query Notification Profiler Event Schema](https://go.microsoft.com/fwlink/?LinkId=63331) page.|1|Yes|  
  
  
