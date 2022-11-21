---
title: "sys.dm_xe_session_event_actions (Transact-SQL)"
description: sys.dm_xe_session_event_actions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_xe_session_event_actions_TSQL"
  - "sys.dm_xe_session_event_actions_TSQL"
  - "dm_xe_session_event_actions"
  - "sys.dm_xe_session_event_actions"
helpviewer_keywords:
  - "extended events [SQL Server], views"
  - "sys.dm_xe_session_event_actions dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 0c22a546-683e-4c84-ab97-1e9e95304b03
---
# sys.dm_xe_session_event_actions (Transact-SQL)
[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information about event session actions for *active* server-scoped sessions. Actions are executed when events are fired.

Azure SQL Database supports only database-scoped sessions. See [sys.dm_xe_database_session_event_actions](sys-dm-xe-database-session-event-actions-azure-sql-database.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|action_name|**nvarchar(256)**|The name of the action. Is not nullable.|  
|action_package_guid|**uniqueidentifier**|The GUID for the package that contains the action. Is not nullable.|  
|event_name|**nvarchar(256)**|The name of the event that the action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package that contains the event. Is not nullable.|  
  
## Permissions  

Requires VIEW SERVER STATE permission on the server.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_session_event_actions.event_session_address|sys.dm_xe_sessions.address|Many-to-one|  
|sys.dm_xe_session_event_actions.action_name,<br /><br /> sys.dm_xe_session_event_actions.action_package_guid|sys.dm_xe_objects.name,<br /><br /> sys.dm_xe_session_events.event_package_guid|Many-to-one|  
|sys.dm_xe_session_event_actions.event_name,<br /><br /> sys.dm_xe_session_event_actions.event_package_guid|sys.dm_xe_objects.name,<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
