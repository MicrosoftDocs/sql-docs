---
title: "sys.dm_xe_database_sessions"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 33ea5179-16bb-4abd-96cc-9bc696e80987
monikerRange: "=azuresqldb-current"
---
# sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about *active* database-scoped extended events sessions. A session is a collection of events, actions, and targets. For information on all event sessions in the database, see [sys.database_event_sessions](../system-catalog-views/sys-database-event-sessions-azure-sql-database.md).

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
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
|session_source|**nvarchar(256)**| The *database_name* that is the scope of the session. |  
|buffer_processed_count|**bigint**|The total number of buffers processed by the session and accumulates from start of session. Is not nullable.|
|buffer_full_count|**bigint**|The number of buffers that were full when they were processed and accumulates from start of session. Is not nullable.|  
|total_bytes_generated|**bigint**|The number of actual bytes that the extended events session has generated. This information is collected when the session is processing buffers and applies to the file target only. No tracking for other targets. | 
|total_target_memory |**bigint**|The total target memory in bytes for a session storing information in a ring buffer target. Is not nullable.|
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
## Next steps

Learn more about related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
