---
title: "sys.database_event_session_actions"
description: The sys.database_event_session_actions dynamic management view (DMV) returns a row for each action on each event of a database-scoped event session.
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
# sys.database_event_session_actions
[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

The `sys.database_event_session_actions` dynamic management view (DMV) returns a row for each action on each event of a database-scoped event session. For information on actions in *active* database-scoped event sessions, see [sys.dm_xe_database_session_event_actions](../system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database.md).

- Azure SQL Database and SQL database in Fabric support only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). 
- Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for SQL managed instances. For more information, see [CREATE EVENT SESSION code examples](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|`event_session_id`|**int**|The ID of the event session. Is not nullable.|  
|`event_id`|**int**|The ID of the event. This ID is unique within the event session object. Is not nullable.|  
|`name`|**sysname**|The name of the action. Is nullable.|  
|`package`|**sysname**|The name of the event package that contains the event. Is nullable.|  
|`module`|**sysname**|The name of the module that contains the event. Is nullable.|  
  
## Permissions  

Requires the VIEW DATABASE PERFORMANCE STATE permission.  
  
## Remarks

This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | ---- | ------------ |
|`sys.database_event_session_actions.event_session_id`|`sys.database_event_sessions.event_session_id`|Many to one|  
|`sys.database_event_session_actions.event_id`<br /><br /> `sys.database_event_session_actions.event_session_id`|`sys.database_event_session_events.event_session_id`<br /><br /> `sys.database_event_session_events.event_id`|Many to one|  

## Related content

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Monitoring Microsoft Azure SQL Database performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended Events overview](../extended-events/extended-events.md)
