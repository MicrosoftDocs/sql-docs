---
title: "Data File Auto Grow Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Data File Auto Grow event class"
ms.assetid: 63701c20-7886-454a-936f-7aea9d042cf7
author: stevestein
ms.author: sstein
manager: craigg
---
# Data File Auto Grow Event Class
  The **Data File Auto Grow** event class indicates that the data file grew automatically. This event is not triggered if the data file is grown explicitly by using the ALTER DATABASE statement.  
  
 Include the **Data File Auto Grow** event class in traces that are monitoring growth of the data file.  
  
 When the **Data File Auto Grow** event class is included in a trace, the amount of overhead incurred is low unless the data file is growing automatically frequently.  
  
## Data File Auto Grow Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**ApplicationName**|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**ClientProcessID**|**int**|ID assigned by the host computer to the process where the client application is running. This data column is populated if the client provides the client process ID.|9|Yes|  
|**DatabaseID**|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**Duration**|**bigint**|Length of time (in milliseconds) necessary to extend the file.|13|Yes|  
|**EndTime**|**datetime**|Time that the data file auto grow ended.|18|Yes|  
|**EventClass**|**int**|Type of event = 92.|27|No|  
|**EventSequence**|**int**|Sequence of the **CursorClose** event class in the batch.|51|No|  
|**Filename**|**nvarchar**|Logical name of the file being extended.|36|Yes|  
|**HostName**|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IntegerData**|**int**|Number of 8-kilobyte (KB) pages by which the file increased.|25|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginName**|**nvarchar**|Name of the login of the user (either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security login or the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login credentials in the form of DOMAIN\Username).|11|Yes|  
|**LoginSid**|**image**|Security identifier (SID) of the logged-in user. You can find this information in the **sys.server_principals** catalog view. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows domain to which the user belongs.|7|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user that originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**Int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
