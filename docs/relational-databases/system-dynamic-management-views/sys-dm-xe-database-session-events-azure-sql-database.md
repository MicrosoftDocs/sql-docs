---
title: "sys.dm_xe_database_session_events"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_session_events (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/28/2022"
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-lt-2019
dev_langs:
  - "TSQL"
ms.assetid: 9e985a19-f93f-4c56-b644-12c529298011
monikerRange: "=azuresqldb-current"
---
# sys.dm_xe_database_session_events (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about session events for *active* database-scoped sessions. Events are discrete execution points. Predicates can be applied to events to stop them from firing if the event does not contain the required information. For information on events in all database-scoped sessions, see [sys.database_event_session_events](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md).

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that an action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package containing the event. Is not nullable.|  
|event_predicate|**nvarchar(2048)**|An XML representation of the predicate tree that is applied to the event. Is nullable.|  
|event_fire_count|**bigint**|Internal use only.|  
|event_fire_average_time|**bigint**|Internal use only.|  
|event_fire_min_time|**bigint**|The minimum time taken to publish the event, in microseconds. Is not nullable.|  
|event_fire_max_time|**bigint**|The maximum time taken to publish the event, in microseconds. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_events.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_events.event_package_guid, sys.dm_xe_database_session_events.event_name|sys.dm_xe_objects.name, sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
