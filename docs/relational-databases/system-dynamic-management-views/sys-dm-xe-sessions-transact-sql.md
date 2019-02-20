---
title: "sys.dm_xe_sessions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_xe_sessions_TSQL"
  - "dm_xe_sessions"
  - "sys.dm_xe_sessions_TSQL"
  - "sys.dm_xe_sessions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_xe_sessions dynamic management view"
  - "extended events [SQL Server], views"
ms.assetid: defd6efb-9507-4247-a91f-dc6ff5841e17
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_xe_sessions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about an active extended events session. This session is a collection of events, actions, and targets.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|address|**varbinary(8)**|The memory address of the session. address is unique across the local system. Is not nullable.|  
|name|**nvarchar(256)**|The name of the session. name is unique across the local system. Is not nullable.|  
|pending_buffers|**int**|The number of full buffers that are pending processing. Is not nullable.|  
|total_regular_buffers|**int**|The total number of regular buffers that are associated with the session. Is not nullable.<br /><br /> Note: Regular buffers are used most of the time. These buffers are of sufficient size to hold many events. Typically, there will be three or more buffers per session. The number of regular buffers is automatically determined by the server, based on the memory partitioning that is set through the MEMORY_PARTITION_MODE option. The size of the regular buffers is equal to the value of the MAX_MEMORY option (default of 4 MB), divided by the number of buffers. For more information about the MEMORY_PARTITION_MODE and the MAX_MEMORY options, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md).|  
|regular_buffer_size|**bigint**|The regular buffer size, in bytes. Is not nullable.|  
|total_large_buffers|**int**|The total number of large buffers. Is not nullable.<br /><br /> Note: Large buffers are used when an event is larger than a regular buffer. They are set aside explicitly for this purpose. Large buffers are allocated when the event session starts, and are sized according to the MAX_EVENT_SIZE option. For more information about the MAX_EVENT_SIZE option, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md).|  
|large_buffer_size|**bigint**|The large buffer size, in bytes. Is not nullable.|  
|total_buffer_size|**bigint**|The total size of the memory buffer that is used to store events for the session, in bytes. Is not nullable.|  
|buffer_policy_flags|**int**|A bitmap that indicates how session event buffers behave when all the buffers are full and a new event is fired. Is not nullable.|  
|buffer_policy_desc|**nvarchar(256)**|A description that indicates how session event buffers behave when all the buffers are full and a new event is fired.  Is not nullable. buffer_policy_desc  can be one of the following:<br /><br /> Drop event<br /><br /> Do not drop events<br /><br /> Drop full buffer<br /><br /> Allocate new buffer|  
|flags|**int**|A bitmap that indicates the flags that have been set on the session. Is not nullable.|  
|flag_desc|**nvarchar(256)**|A description of the flags set on the session.  Is not nullable. flag_desc can be any combination of the following:<br /><br /> Flush buffers on close<br /><br /> Dedicated dispatcher<br /><br /> Allow recursive events|  
|dropped_event_count|**int**|The number of events that were dropped when the buffers were full. This value is **0** if the buffer policy is "Drop full buffer" or "Do not drop events". Is not nullable.|  
|dropped_buffer_count|**int**|The number of buffers that were dropped when the buffers were full. This value is **0** if the buffer policy is set to "Drop event" or "Do not drop events". Is not nullable.|  
|blocked_event_fire_time|**int**|The length of time that event firings were blocked when buffers were full. This value is **0** if the buffer policy is "Drop full buffer" or "Drop event". Is not nullable.|  
|create_time|**datetime**|The time that the session was created. Is not nullable.|  
|largest_event_dropped_size|**int**|The size of the largest event that did not fit into the session buffer. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  

