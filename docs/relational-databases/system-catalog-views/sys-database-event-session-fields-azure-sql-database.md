---
title: "sys.database_event_session_fields"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.database_event_session_fields (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 9b5c94d6-612c-4e0f-976d-ac6ba55da3ac
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_event_session_fields (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE [sqlserver2016-asdb](../../includes/applies-to-version/sqlserver2016-asdb.md)]

Returns a row for each customizable column that was explicitly set on [events](sys-database-event-session-events-azure-sql-database.md) and [targets](sys-database-event-session-targets-azure-sql-database.md) in a database-scoped event session.

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|object_id|**int**|The ID of the object this field is associated with. Is not nullable.|  
|name|**sysname**|The name of the field. Is not nullable.|  
|value|**sql_variant**|The value of the field. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
## Remarks

This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.database_event_session_actions.event_session_id|sys.database_event_sessions.event_session_id|Many to one|  
|sys.database_event_session_actions.event_id<br /><br /> sys.database_event_session_actions.object_id<br /><br /> sys.database_event_session_actions.event_session_id|sys.database_event_session_events.event_session_id<br /><br /> sys.database_event_session_events.event_id|Many to one|  
|sys.database_event_session_actions.event_session_id<br /><br /> sys.database_event_session_actions.object_id|sys.database_event_session_targets.event_session_id<br /><br /> sys.database_event_session_targets.target_id|Many to one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.database_event_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions (Azure SQL Database and Azure SQL Managed Instance)](sys-database-event-session-actions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
