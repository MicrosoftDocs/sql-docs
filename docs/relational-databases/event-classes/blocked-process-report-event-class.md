---
title: "Blocked Process Report Event Class | Microsoft Docs"
description: The Blocked Process Report event class indicates that a task has been blocked for more than a specified amount of time in SQL Server.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Blocked Process Report event class"
ms.assetid: e8acb408-938d-4b36-81dd-04f087410cc5
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Blocked Process Report Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **Blocked Process Report** event class indicates that a task has been blocked for more than a specified amount of time. This event class does not include system tasks or tasks that are waiting on non deadlock-detectable resources.  
  
 To configure the threshold and frequency at which reports are generated, use the **sp_configure** command to configure the **blocked process threshold** option, which can be set in seconds. By default, no blocked process reports are produced. For more information about setting the **blocked process threshold** option, see [blocked process threshold Server Configuration Option](../../database-engine/configure-windows/blocked-process-threshold-server-configuration-option.md).  
  
 For information about filtering the data returned by the **Blocked Process Report** event class, see [Filter Events in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md), [Set a Trace Filter &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/set-a-trace-filter-transact-sql.md), or [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md).  
  
## Blocked Process Report Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**DatabaseID**|**int**|ID of the database in which the lock was acquired. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**Duration**|**bigint**|The amount of time (in microseconds) that the process was blocked.|13|Yes|  
|**EndTime**|**datetime**|Time at which the event ended. This column is not populated for starting event classes, such as **SQL:BatchStarting** or **SP:Starting**.|15|Yes|  
|**EventClass**|**int**|Type of event = 137.|27|No|  
|**EventSequence**|**int**|The sequence of a given event within the request.|51|No|  
|**IndexID**|**int**|ID for the index on the object affected by the event. To determine the index ID for an object, use the **indid** column of the **sysindexes** system table.|24|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginSid**|**image**|Security identifier (SID) of the logged-in user. This event is always reported from the system thread. IsSystem = 1; SID = sa.|41|Yes|  
|**Mode**|**int**|The state the event has received or is requesting.<br /><br /> 0=NULL<br /><br /> 1=Sch-S<br /><br /> 2=Sch-M<br /><br /> 3=S<br /><br /> 4=U<br /><br /> 5=X<br /><br /> 6=IS<br /><br /> 7=IU<br /><br /> 8=IX<br /><br /> 9=SIU<br /><br /> 10=SIX<br /><br /> 11=UIX<br /><br /> 12=BU<br /><br /> 13=RangeS-S<br /><br /> 14=RangeS-U<br /><br /> 15=RangeI-N<br /><br /> 16=RangeI-S<br /><br /> 17=RangeI-U<br /><br /> 18=RangeI-X<br /><br /> 19=RangeX-S<br /><br /> 20=RangeX-U<br /><br /> 21=RangeX-X|32|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object on which the lock was acquired, if available and applicable.|22|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26||  
|**SessionLoginName**|**nvarchar**|The login name of the user that originated the session. For example, if you connect to SQL Server using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1; and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**TextData**|**ntext**|Text value dependent on the event class captured in the trace.|1|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
