---
title: "Background Job Error Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Background Job Error event class"
ms.assetid: 9e6d2a0e-919d-4fe2-a306-b20b8d41c197
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Background Job Error Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **Background Job Error** event class occurs when a background job has terminated abnormally. This condition might require the attention of a system administrator.  
  
## Background Job Error Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**DatabaseID**|**int**|ID of the database specified by job. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**Error**|**int**|Error number of the last attempt (**EventSubClass** 1 only).|31|Yes|  
|**EventClass**|**int**|Type of event = 193.|27|No|  
|**EventSequence**|**int**|The sequence of a given event within the request.|51|No|  
|**EventSubClass**|**int**|Type of event subclass.<br /><br /> 1 = Background job giving up after failure.<br /><br /> 2 = Background job dropped - queue is full.<br /><br /> 3 = Background job returned an error.|21|Yes|  
|**IndexID**|**int**|ID for the index on the object affected by the event. To determine the index ID for an object, use the **indid** column of the **sysindexes** system table.|24|Yes|  
|**IntegerData**|**int**|Number of tries attempted by the job (**EventSubClass** 1 only).|25|Yes|  
|**IntegerData2**|**int**|Job sequence number.|55|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object.|22|Yes|  
|**SessionLoginName**|**nvarchar**|Login name of the user that originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows logins.|64|Yes|  
|**Severity**|**int**|Severity level of the error on the last attempt (**EventSubClass** 1 only).|20|Yes|  
|**StartTime**|**datetime**|Time at which the job was created.|14|Yes|  
|**State**|**int**|State of the error on the last attempt (**EventSubClass** 1 only).|30|Yes|  
|**TextData**|**ntext**|Text description of the event subclass value.|1|Yes|  
|**Type**|**int**|Type of job.|57|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [Auto Stats Event Class](../../relational-databases/event-classes/auto-stats-event-class.md)  
  
  
