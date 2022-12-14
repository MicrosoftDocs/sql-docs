---
description: "Database Mirroring State Change Event Class"
title: "Database Mirroring State Change Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "event notifications [SQL Server], database mirroring"
  - "database mirroring [SQL Server], event notifications"
  - "Database Mirroring State Change event class"
ms.assetid: f936a99e-2a81-4768-8177-5c969bbe2e04
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Mirroring State Change Event Class
[!INCLUDE [SQL Server - ASDB](../../includes/applies-to-version/sql-asdb.md)]
  The **Database Mirroring State Change** event class indicates when the state of a mirrored database changes. Include this event class in traces that are monitoring conditions of mirrored databases.  
  
 When the **Database Mirroring State Change** event class is included in a trace the relative overhead is low. The overhead may be greater if the state of the mirrored databases increase.  
  
## Data Database Mirroring State Change Event Class Data Columns  
  
|Data Column Name|Data Type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**DatabaseID**|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the mirrored database.|35|Yes|  
|**EventClass**|**int**|Type of event = 167.|27|No|  
|**EventSequence**|**int**|Sequence of event class in batch.|51|No|  
|**IntegerData**|**int**|Prior state ID.|25|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginSid**|**image**|Security identification number (SID) of the logged-in user. You can find this information in the **sys.server_principals** catalog view. Each SID is unique for each login in the server.|41|Yes|  
|**RequestID**|**int**|ID of the request containing the statement.|49|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**State**|**int**|New mirroring state ID:<br /><br /> 0 = Null Notification<br /><br /> 1 = Synchronized Principal with Witness<br /><br /> 2 = Synchronized Principal without Witness<br /><br /> 3 = Synchronized Mirror with Witness<br /><br /> 4 = Synchronized Mirror without Witness<br /><br /> 5 = Connection with Principal Lost<br /><br /> 6 = Connection with Mirror Lost<br /><br /> 7 = Manual Failover<br /><br /> 8 = Automatic Failover<br /><br /> 9 = Mirroring Suspended<br /><br /> 10 = No Quorum<br /><br /> 11 = Synchronizing Mirror<br /><br /> 12 = Principal Running Exposed|30|Yes|  
|**TextData**|**ntext**|Description of the state change.|1|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
