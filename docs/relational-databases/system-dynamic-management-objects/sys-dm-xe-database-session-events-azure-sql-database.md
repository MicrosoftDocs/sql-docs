---
title: "sys.dm_xe_database_session_events"
description: The sys.dm_xe_database_session_events dynamic management view (DMV) returns information about session events for active database-scoped sessions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_xe_database_session_events
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/asdb-asmi-fabricsqldb.md)]

The `sys.dm_xe_database_session_events` dynamic management view (DMV) returns information about session events for *active* database-scoped sessions. Events are discrete execution points. Predicates can be applied to events to stop them from firing if the event does not contain the required information. For information on events in all database-scoped sessions, see [sys.database_event_session_events](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md).

- Azure SQL Database and SQL database in Fabric support only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). 
- Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for SQL managed instances. For more information, see [CREATE EVENT SESSION code examples](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|`event_session_address`|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|`event_name`|**nvarchar(60)**|The name of the event that an action is bound to. Is not nullable.|  
|`event_package_guid`|**uniqueidentifier**|The GUID for the package containing the event. Is not nullable.|  
|`event_predicate`|**nvarchar(2048)**|An XML representation of the predicate tree that is applied to the event. Is nullable.|  
|`event_fire_count`|**bigint**|Internal use only.|  
|`event_fire_average_time`|**bigint**|Internal use only.|  
|`event_fire_min_time`|**bigint**|The minimum time taken to publish the event, in microseconds. Is not nullable.|  
|`event_fire_max_time`|**bigint**|The maximum time taken to publish the event, in microseconds. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`sys.dm_xe_database_session_events.event_session_address`|`sys.dm_xe_database_sessions.address`|Many-to-one|  
|`sys.dm_xe_database_session_events.event_package_guid`, `sys.dm_xe_database_session_events.event_name`|`sys.dm_xe_objects.name`, `sys.dm_xe_objects.package_guid`|Many-to-one|  
  
## Related content

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
