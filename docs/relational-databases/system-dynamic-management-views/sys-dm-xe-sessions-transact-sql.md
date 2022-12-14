---
title: "sys.dm_xe_sessions (Transact-SQL)"
description: sys.dm_xe_sessions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_xe_sessions_TSQL"
  - "dm_xe_sessions"
  - "sys.dm_xe_sessions_TSQL"
  - "sys.dm_xe_sessions"
helpviewer_keywords:
  - "sys.dm_xe_sessions dynamic management view"
  - "extended events [SQL Server], views"
dev_langs:
  - "TSQL"
---
# sys.dm_xe_sessions (Transact-SQL)
[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information about server-scoped active extended events sessions. A session is a collection of events, actions, and targets.  

Azure SQL Database supports only database-scoped sessions. See [sys.dm_xe_database_sessions](sys-dm-xe-database-sessions-azure-sql-database.md).
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|address|**varbinary(8)**|The memory address of the session. `address` is unique across the local system. Is not nullable.|  
|name|**nvarchar(256)**|The name of the session. `name` is unique across the local system. Is not nullable.|  
|pending_buffers|**int**|The number of full buffers that are pending processing. Is not nullable.|  
|total_regular_buffers|**int**|The total number of regular buffers that are associated with the session. Is not nullable.<br /><br /> Note: Regular buffers are used most of the time. These buffers are of sufficient size to hold many events. Typically, there will be three or more buffers per session. The number of regular buffers is automatically determined by the server, based on the memory partitioning that is set through the MEMORY_PARTITION_MODE option. The size of the regular buffers is equal to the value of the MAX_MEMORY option (default of 4 MB), divided by the number of buffers. For more information about the MEMORY_PARTITION_MODE and the MAX_MEMORY options, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md).|  
|regular_buffer_size|**bigint**|The regular buffer size, in bytes. Is not nullable.|  
|total_large_buffers|**int**|The total number of large buffers. Is not nullable.<br /><br /> Note: Large buffers are used when an event is larger than a regular buffer. They are set aside explicitly for this purpose. Large buffers are allocated when the event session starts, and are sized according to the MAX_EVENT_SIZE option. For more information about the MAX_EVENT_SIZE option, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md).|  
|large_buffer_size|**bigint**|The large buffer size, in bytes. Is not nullable.|  
|total_buffer_size|**bigint**|The total size of the memory buffer that is used to store events for the session, in bytes. Is not nullable.|  
|buffer_policy_flags|**int**|A bitmap that indicates how session event buffers behave when all the buffers are full and a new event is fired. Is not nullable.|  
|buffer_policy_desc|**nvarchar(256)**|A description that indicates how session event buffers behave when all the buffers are full and a new event is fired.  Is not nullable. `buffer_policy_desc` can be one of the following values:<br /><br /> Drop event<br />Do not drop events<br />Drop full buffer<br />Allocate new buffer|  
|flags|**int**|A bitmap that indicates the flags that have been set on the session. Is not nullable.|  
|flag_desc|**nvarchar(256)**|A description of the flags set on the session.  Is not nullable. `flag_desc` can be any combination of the following values:<br /><br /> Flush buffers on close<br />Dedicated dispatcher<br />Allow recursive events|  
|dropped_event_count|**int**|The number of events that were dropped when the buffers were full. This value is **0** if `buffer_policy_desc` is "Drop full buffer" or "Do not drop events". Is not nullable.|  
|dropped_buffer_count|**int**|The number of buffers that were dropped when the buffers were full. This value is **0** if `buffer_policy_desc` is set to "Drop event" or "Do not drop events". Is not nullable.|  
|blocked_event_fire_time|**int**|The length of time that event firings were blocked when buffers were full. This value is **0** if `buffer_policy_desc` is "Drop full buffer" or "Drop event". Is not nullable.|  
|create_time|**datetime**|The time that the session was created. Is not nullable.|  
|largest_event_dropped_size|**int**|The size of the largest event that did not fit into the session buffer. Is not nullable.|  
|session_source|**nvarchar(256)**| The scope of the session. Is not nullable. `session_source` can be one of the following values:<BR><BR>server = session scoped to the server, including user sessions.<BR>internal = certain internal sessions, such as the `sp_server_diagnostics` session.|  
|buffer_processed_count|**bigint**|**Applies to:** SQL Server 2017 and later. The total number of buffers processed by the session and accumulates from start of session. Is not nullable.|
|buffer_full_count|**bigint**|**Applies to:** SQL Server 2017 and later. The number of buffers that were full when they were processed and accumulates from start of session. Is not nullable.|  
|total_bytes_generated|**bigint**|**Applies to:** SQL Server 2017 and later. The number of actual bytes that the extended events session has generated. This information is collected when the session is processing buffers and applies to the file target only. No tracking for other targets. |  
|total_target_memory |**bigint**|**Applies to:** SQL Server 2019 and later. The total target memory in bytes for a session storing information in a ring buffer target. Is not nullable.|

## Permissions

Requires VIEW SERVER STATE permission on the server.  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_session_targets (Transact-SQL)](sys-dm-xe-session-targets-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
