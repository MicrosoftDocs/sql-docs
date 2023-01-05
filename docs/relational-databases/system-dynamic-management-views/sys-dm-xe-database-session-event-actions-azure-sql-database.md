---
title: "sys.dm_xe_database_session_event_actions"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_session_event_actions (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-lt-2019
dev_langs:
  - "TSQL"
ms.assetid: 48519fd9-c7c2-434b-848d-ccbf41133fdd
monikerRange: "=azuresqldb-current"
---
# sys.dm_xe_database_session_event_actions (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about event session actions for *active* database-scoped sessions. Actions are executed when events are fired. This management view aggregates statistics about the number of times an action has run, and the total run time of the action. For information on all actions in database-scoped sessions, see [sys.database_event_session_actions](../system-catalog-views/sys-database-event-session-actions-azure-sql-database.md).

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|action_name|**nvarchar(60)**|The name of the action. Is not nullable.|  
|action_package_guid|**uniqueidentifier**|The GUID for the package that contains the action. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that the action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package that contains the event. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_event_actions.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_event_actions.action_name<br /><br /> sys.dm_xe_session_event_actions.action_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_database_session_events.event_package_guid|Many-to-one|  
|sys.dm_xe_database_session_event_actions.event_name<br /><br /> sys.dm_xe_database_session_event_actions.event_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
