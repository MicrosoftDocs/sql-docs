---
title: "sys.dm_xe_session_events (Transact-SQL)"
description: sys.dm_xe_session_events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/28/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_xe_session_events"
  - "sys.dm_xe_session_events_TSQL"
  - "dm_xe_session_events"
  - "dm_xe_session_events_TSQL"
helpviewer_keywords:
  - "sys.dm_xe_session_events dynamic management view"
  - "extended events [SQL Server], views"
dev_langs:
  - "TSQL"
ms.assetid: 4f027b31-4e03-43a6-849d-1ba9d8d34ae8
---
# sys.dm_xe_session_events (Transact-SQL)
[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information about events for *active* server-scoped sessions. Events are discrete execution points. Predicates can be applied to events to stop them from firing if the event does not contain the required information.

Azure SQL Database supports only database-scoped sessions. See [sys.dm_xe_database_session_events](sys-dm-xe-database-session-events-azure-sql-database.md).
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|event_name|**nvarchar(256)**|The name of the event that an action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package containing the event. Is not nullable.|  
|event_predicate|**nvarchar(3072)**|An XML representation of the predicate tree that is applied to the event. Is nullable.|  
|event_fire_count|**bigint**|The number of times the event has fired (was published) since the session was started. Is not nullable. Applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.|  
|event_fire_average_time|**bigint**|The average time taken to publish the event, in microseconds. Is not nullable. Applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.|  
|event_fire_min_time|**bigint**|The minimum time taken to publish the event, in microseconds. Is not nullable. Applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.|  
|event_fire_max_time|**bigint**|The maximum time taken to publish the event, in microseconds. Is not nullable. Applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later.|  
  
> [!NOTE]
> The `event_fire_count` and `event_fire_average_time` columns are populated only when trace flag 9708 is enabled.

## Permissions

Requires VIEW SERVER STATE permission on the server.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_session_events.event_session_address|sys.dm_xe_sessions.address|Many-to-one|  
|sys.dm_xe_session_events.event_package_guid,<br /><br /> sys.dm_xe_session_events.event_name|sys.dm_xe_objects.name,<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_event_actions (Transact-SQL)](sys-dm-xe-session-event-actions-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
