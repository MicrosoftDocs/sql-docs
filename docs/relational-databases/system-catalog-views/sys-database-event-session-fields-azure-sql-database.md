---
title: "sys.database_event_session_fields"
description: The sys.database_event_session_fields dynamic management view (DMV) returns a row for each customizable column that was explicitly set on events and targets in a database-scoped event session.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.database_event_session_fields

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

The `sys.database_event_session_fields` dynamic management view (DMV) returns a row for each customizable column that was explicitly set on [events](sys-database-event-session-events-azure-sql-database.md) and [targets](sys-database-event-session-targets-azure-sql-database.md) in a database-scoped event session.

- Azure SQL Database and SQL database in Fabric support only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). 
- Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for SQL managed instances. For more information, see [CREATE EVENT SESSION code examples](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|`event_session_id`|**int**|The ID of the event session. Is not nullable.|  
|`object_id`|**int**|The ID of the object this field is associated with. Is not nullable.|  
|`name`|**sysname**|The name of the field. Is not nullable.|  
|`value`|**sql_variant**|The value of the field. Is not nullable.|  

## Permissions

Requires the VIEW DATABASE PERFORMANCE STATE permission.  

## Remarks

This view has the following relationship cardinalities.  

| From | To | Relationship |
| ---- | ---- | ------------ |
|`sys.database_event_session_actions.event_session_id`|`sys.database_event_sessions.event_session_id`|Many to one|
|`sys.database_event_session_actions.event_id`<br /><br /> `sys.database_event_session_actions.object_id`<br /><br /> `sys.database_event_session_actions.event_session_id`|`sys.database_event_session_events.event_session_id`<br /><br /> `sys.database_event_session_events.event_id`|Many to one|  
|`sys.database_event_session_actions.event_session_id`<br /><br /> `sys.database_event_session_actions.object_id`|`sys.database_event_session_targets.event_session_id`<br /><br /> `sys.database_event_session_targets.target_id`|Many to one|  

## Related content

- [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file)
- [sys.database_event_sessions](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions](sys-database-event-session-actions-azure-sql-database.md)
- [Monitor performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
